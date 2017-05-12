using System;
using MvvmCross.Core.ViewModels;

namespace HelloWorld.Core.ViewModels
{
    public class NotifiedViewModel : MvxViewModel
    {
        private int medId;
        public int MedicationId
        {
            get { return this.medId; }
            set { this.SetProperty (ref this.medId, value); }
        }

        public NotifiedViewModel ()
        {
        }

        public void Init(int medicationId)
        {
            this.MedicationId = medicationId;
        }
    }
}
