using EnglishSelfTraining.ExtensionMethods;
using EnglishSelfTraining.Objects;
using EnglishSelfTraining.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EnglishSelfTraining.StaticHelpers
{
    public static class TimeFactory
    {
        public static Month GetCurrentMonth() => DateTime.Now
            .Use(now => new Month(now.ToString("MMMM", CultureInfo.InvariantCulture), now.Year, now.Month));
        public static bool IsFirstDayOfMonth() => DateTime.Now.Day == 1;
    }
}
