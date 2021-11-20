using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.entities
{
    public class C_bill
    {
        public int id { set; get; }
        public int client_bill_id { set; get; }
        public string description { set; get; }
        public int value { set; get; }
    }
}
