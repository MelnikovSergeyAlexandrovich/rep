using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsIS
{
    public class RegistrationLog
    {
        public string status { get; set; }
        public DateTime time { get; set; }  
        public int id { get; set; }

        public RegistrationLog(string status, DateTime time, int id)
        {
            this.status = status;
            this.time = time;
            this.id = id;
        }
    }
}
