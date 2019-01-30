using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.Business.API
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public JToken Result { get; set; }
    }
}
