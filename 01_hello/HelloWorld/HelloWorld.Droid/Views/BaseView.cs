using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Core.ViewModels;
using System.Reactive.Disposables;

namespace HelloWorld.Droid.Views
{
    public abstract class BaseView : MvxAppCompatActivity
    {
        protected Toolbar Toolbar { get; set; }

        protected CompositeDisposable Disposables { get; } = new CompositeDisposable ();

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (LayoutResource);

            Toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
            if (Toolbar != null) {
                SetSupportActionBar (Toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled (true);
                SupportActionBar.SetHomeButtonEnabled (true);
            }
        }

        protected abstract int LayoutResource { get; }

        protected override void OnDestroy ()
        {
            base.OnDestroy ();
            this.Disposables.Dispose ();
        }
    }

    public abstract class BaseView<T> : BaseView where T : IMvxViewModel
    {
        public new T ViewModel
        {
            get { return (T)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
