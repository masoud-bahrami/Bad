﻿using System;

namespace Bad.Code.BadSmells._03LongFunction
{
    public static class DateTimeExtensions
    {
        public static bool IsBefore(this DateTime dateTime, DateTime secondDateTime)
        {
            return true;
        }
        public static bool IsAfter(this DateTime dateTime, DateTime secondDateTime)
        {
            return true;
        }
    }
}