﻿using System;

namespace BLL.Infrastructure
{
    public static class TokenConfig
    {
        public static string Issuer = "forum";
        public static string Audience = "angular";
        public static string Key = "234kji478bt6tefdhddgfdrdtrdlkjijbff_ju9";
        public static DateTime LifeTime = DateTime.Now.AddDays(1);
    }
}
