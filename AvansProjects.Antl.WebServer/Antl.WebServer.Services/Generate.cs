using System;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Services
{
    internal static class Generate

    {
        public static string ExternalId()
        {
            return string.Join("-", new Random().Next(1000, 9999).ToString(),
                new Random().Next(1000, 9999).ToString(), new Random().Next(1000, 9999).ToString());
        }

        public static int Hash(IEntity entity)
        {
            return entity.GetHashCode();
        }
    }
}