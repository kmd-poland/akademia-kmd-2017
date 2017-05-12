using System;
using MvvmCross.Plugins.Messenger;
namespace HelloWorld.Core.Domain
{

	public class SetTitleMessage : MvxMessage
	{
		public string Title { get; }

		public SetTitleMessage(object sender, string value) : base(sender)
		{
			this.Title = value;
		}
	}
}
