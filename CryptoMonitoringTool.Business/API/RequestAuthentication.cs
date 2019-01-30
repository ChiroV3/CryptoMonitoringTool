using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.Business.API
{
    public class RequestAuthentication
    {
        public string Uri { get; set; }
        public string Hash { get; set; }

        public RequestAuthentication(string uri, string hash)
        {
            this.Uri = uri;
            this.Hash = hash;
        }

        
    }
}
