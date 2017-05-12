using System;
using Android.App;
using HelloWorld.Core.ViewModels;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using Android.OS;

namespace HelloWorld.Droid.Views
{
    [Activity (Label = "View for FirstViewModel")]
    public class NotifiedView : BaseView<NotifiedViewModel>
    {
        private TextView medicationIdTextView;

        public NotifiedView ()
        {
        }

        protected override int LayoutResource => Resource.Layout.NotifiedView;

        protected override void OnCreate (Bundle bundle)
        {
        	base.OnCreate (bundle);

        	//SupportActionBar.SetDisplayHomeAsUpEnabled (false);

            this.medicationIdTextView = this.FindViewById<TextView> (Resource.Id.textView1);
        	
        	this.SetBindings ();
        }

        private void SetBindings ()
        {
            var bindingSet = this.CreateBindingSet<NotifiedView, NotifiedViewModel> ();
            bindingSet
                .Bind (this.medicationIdTextView)
                .To (vm => vm.MedicationId);
        }
    }
}
