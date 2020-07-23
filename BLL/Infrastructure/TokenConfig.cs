using System;

namespace BLL.Infrastructure
{
    public static class TokenConfig
    {
        public static readonly string Issuer = "forum";
        public static readonly string Audience = "angular";
        public static readonly string Key = "234kji478bt6tefdhddgfdrdtrdlkjijbff_ju9";
        public static DateTime LifeTime = DateTime.Now.AddDays(1);
    }
}
