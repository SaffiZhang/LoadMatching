using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Caching
{
    public class RedisConfiguration
    {
        public string Server { get; set; }
        public string Password { get; set; }
        public int ConnectTimeout { get; set; } = 5000; // milliseconds
        public int SyncTimeout { get; set; } = 5000; // milliseconds
    }
}
