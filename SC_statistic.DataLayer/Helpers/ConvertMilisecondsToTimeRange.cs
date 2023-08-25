using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_statistic.DataLayer.Helpers
{
    public static class ConvertMilisecondsToTimeRange
    {
        public static string Convert(long milisecondsCount)
        {
            long secondsCount = milisecondsCount/1000;
            int days = (int)(secondsCount / 86400);
            int hours = (int)((secondsCount % 86400) / 3600);
            int minutes = (int)(((secondsCount % 86400) % 3600) / 60);
            int seconds = (int)(((secondsCount % 86400) % 3600) % 60);

            return $"{days}дн. {hours}ч. {minutes}м. {seconds}с.";
        }
    }
}
