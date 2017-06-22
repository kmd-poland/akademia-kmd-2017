using System;
using Schemas;
using SQLite;

namespace ImporterLekow
{
    public class ProduktLeczniczyOpakowanie
    {
     
        public string SubstancjeCzynne { get; set; }
        public string Substancje { get; set; }


        public string NazwaProduktu { get; set; }

    
        public string RodzajPreparatu { get; set; }

        public string NazwaPowszechnieStosowana { get; set; }

        public string Moc { get; set; }

        public string Postac { get; set; }

        public string PodmiotOdpowiedzielny { get; set; }

	
        public string NumerPozwolenia { get; set; }

        public string WaznoscPozwolenia { get; set; }

        public string KodATC { get; set; }

        public string GatunkiDocelowe { get; set; }

	

        public string OkresyKarencji { get; set; }

        public string Status { get; set; }


		//zawartosc encji Opakowanie


        public string Wielkosc { get; set; }

        public string JednostkaWielkosci { get; set; }

        //EAN jest naszym kluczem glownym!
        [PrimaryKey]
        public string KodEAN { get; set; }

        public opakowanieMTKategoriaDostepnosci? KategoriaDostepnosci { get; set; }

		
        public ProduktLeczniczyOpakowanie()
        {
        }

        // te dwie metody pozwalaja na porownanie obiektow na podstawie EAN (potrzebe do Distinct)
		public override bool Equals(object obj)
		{
            return ((ProduktLeczniczyOpakowanie)obj).KodEAN == KodEAN;
		}
		public override int GetHashCode()
		{
            return KodEAN.GetHashCode();
		}
    }
}
