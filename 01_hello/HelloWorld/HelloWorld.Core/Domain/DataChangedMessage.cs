using System;
using MvvmCross.Plugins.Messenger;
namespace HelloWorld.Core.Domain
{
    public class DataChangedMessage : MvxMessage
    {
        public string Content { get; }

        public DataChangedMessage(object sender, string value) : base(sender)
        {
            this.Content = value;
        }
    }
}
