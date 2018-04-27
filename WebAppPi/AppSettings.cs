using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPi
{
    public class AppSettings
    {
        public string MqttHost { get; set; }
        public int MqttPort { get; set; }
        public string MqttTopic { get; set; }
    }
}
