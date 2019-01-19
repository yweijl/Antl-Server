using System;

namespace Antl.WebServer.Services
{
    internal static class GenerateExternalId
    {
        public static string Generate()
        {
            return string.Join("-", new Random().Next(1000, 9999).ToString(),
                new Random().Next(1000, 9999).ToString(), new Random().Next(1000, 9999).ToString());
        }
    }
}