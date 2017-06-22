using System;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;

namespace HelloWorld.Core.ViewModels
{
    public class ReactiveSearchViewModel : MvxViewModel
    {
        Random rnd = new Random();
        string searchQuery;

        public string SearchQuery
        {
            get
            {
                return searchQuery;
            }

            set
            {
                this.SetProperty(ref searchQuery, value);
            }
        }
        string searchResult;

        public string SearchResult
        {
            get
            {
                return searchResult;
            }

            set
            {
                this.SetProperty(ref searchResult, value);
            }
        }

        public ReactiveSearchViewModel()
        {
            this.WhenAnyValue(x => x.SearchQuery)
                .Throttle(TimeSpan.FromMilliseconds(200)) //reagujemy dopiero, gdy minie 200ms od ostatniej zmiany
                .Select(async query => await Search(query)) //wykonujemy wyszukiwanie
                .Switch() //interesuje nas tylko ostatni wynik. nawet jezeli wczesniejsze wynik przyjdzie pozniej to zostanie zignorowany
                .ObserveOn(RxApp.MainThreadScheduler) ///upewniamy sie, ze wyniki beda przychodzic na watku UI
                .Subscribe(result => this.SearchResult = result);

        }

        private async Task<string> Search(string query)
        {
            //czekamy losowa ilosc czasu - czyli wyniki moga nie przychodzic w kolejnosci
            await Task.Delay(TimeSpan.FromMilliseconds(rnd.Next(100, 300)));
            return $"Result: {query}";
        }
    }
}
