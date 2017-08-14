using System;

namespace DiscordNetPlus.Utils
{
    public static class Uptime
    {
        private static readonly DateTime StartedAt = DateTime.Now;
        public static TimeSpan TimeSpan => DateTime.Now - StartedAt;
        public static string Print
        {
            get
            {
                if (TimeSpan.TotalMinutes < 1)
                    return "< a Minute";
                return $"{(int)TimeSpan.TotalDays} Days {TimeSpan.Hours} Hours {TimeSpan.Minutes} Minutes";
            }
        }

    }
}