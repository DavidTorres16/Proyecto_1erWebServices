using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.entities
{
    public class C_bill_client
    {
        public int Id { get; set; }
        public int client_id { set; get; }
        public int cod { set; get; }
        public List<C_bill> bill_details { set; get; }
    }
}
