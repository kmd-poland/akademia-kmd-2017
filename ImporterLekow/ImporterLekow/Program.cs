using System;
using System.IO;
using System.Xml.Serialization;
using Schemas;
using System.Linq;
using SQLite;

namespace ImporterLekow
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(produktyLecznicze));

            StreamReader reader = new StreamReader("RPL.xml");
            var produktyLecznicze = (produktyLecznicze)serializer.Deserialize(reader);
            reader.Close();


            Console.WriteLine($"Finished reading {produktyLecznicze.produktLeczniczy.Length} records!");

            // denormalize records
            var denormalizedSet =
                produktyLecznicze
                    .produktLeczniczy
                    .Select(p =>
                          p.opakowania.Select(opakowanie =>
                                    new ProduktLeczniczyOpakowanie
                                    {
                                        KodEAN = opakowanie.kodEAN,
                                        NazwaProduktu = p.nazwaProduktu //i tak dalej
                                    })
                           ).SelectMany(x => x)
                    .Where(x=>!string.IsNullOrEmpty(x.KodEAN)) //interesuja nas tylko opakowania z EAN
                    .Distinct() //wystepuje jeden EAN, dla ktorego sa dwa produkty. Pewnie to jakis blad, ale musimy odfiltrowac - wybieramy losowy...
                    .ToList()
                    ;

            //save 'em all
            var connection = new SQLiteConnection("leki.db");
            connection.DropTable<ProduktLeczniczyOpakowanie>();
            connection.CreateTable<ProduktLeczniczyOpakowanie>();
            connection.InsertAll(denormalizedSet);
            connection.Close();

        }
    }
}
