using System;
namespace HelloWorld.Core.Domain
{
    public class RepeatPattern
    {
        public DayOfWeek DayOfWeek { get; set; }
        public RepetitionInterval Interval { get; set; }
        public int RepetitionFrequency { get; set; }

        public RepeatPattern ()
        {
        }
    }
}
