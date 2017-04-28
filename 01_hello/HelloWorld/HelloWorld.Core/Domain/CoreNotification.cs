using System;

namespace HelloWorld.Core.Domain
{
    public class CoreNotification
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public RepeatPattern Pattern { get; set; }

        public CoreNotification (long id, string title, string message, RepeatPattern pattern)
        {
            this.Id = id;
            this.Title = title;
            this.Message = message;
            this.Pattern = pattern;
        }
    }
}
