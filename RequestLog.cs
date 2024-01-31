using System;

namespace AssetsIS
{
    public class RequestLog
    {
        public DateTime time { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public bool status { get; set; }

        public RequestLog(DateTime time, string name, string id, bool status)
        {
            this.time = time;
            this.name = name;
            this.id = id;
            this.status = status;
        }
    }
}
