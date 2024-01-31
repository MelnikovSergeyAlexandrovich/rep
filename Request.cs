using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsIS
{
    public class Request
    {
        public string id { get; set; } // uuid 
        public string name { get; set; }
        public string description { get; set; } 
        public int inventoryID { get; set; }  
        
        public bool status { get; set; }

        public Request(string id, string name, string description, int inventoryID, bool status)
        {
            this.id = id;
            this.name = name;   
            this.description = description;
            this.inventoryID = inventoryID;
            this.status = status;
        }
    }
}
