using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Persistence.Configuration
{
    public class RedisConfiguration
    {
        public string Password { get; set; }
        public bool AllowAdmin { get; set; }
        public bool Ssl { get; set; }
        public int ConnectRetry { get; set; }
        public int Database { get; set; }
        public int ConnectTimeout { get; set; } = 5000; // milliseconds
        public IEnumerable<RedisServer> Hosts { get; set; }
    }
    public class RedisServer
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
