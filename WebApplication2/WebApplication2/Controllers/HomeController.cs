using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using System.Data;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        C_bd BD = new C_bd();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Index()
        {
            List<C_client> client_list = new List<C_client>();

            C_bill bill = new C_bill();
            C_client client = new C_client();
            C_bill_client clients_bill = new C_bill_client();

            return "hola";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public string CreateUser([FromBody] C_client client)
        {
            bool answer = BD.SqlOperations($"insert into Client (name,last_name,bill_id) values ('{client.name}','{client.last_name}',{client.bill_id})");
            if (answer)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }

        public string CreateBill([FromBody] C_bill_client bill_client)
        {
            string sql = $"insert into client_bill (client_id,cod) values ({bill_client.client_id},{bill_client.cod});" + Environment.NewLine;
  
            //sql += "select @@identity as id;" + Environment.NewLine;
            //sql += "set @id_invoice = LAST_INSERT_ID();" + Environment.NewLine;

            foreach (C_bill item in bill_client.bill_details)
            {
                sql += $"insert into bill (client_bill_id,description,value) values ((select max(id) from client_bill),'{item.description}',{item.value});";
            }

            bool answer = BD.SqlOperations(sql);

            if (answer)
            {
                return "Success";
            }
            else
            {
                return "Failed"+sql;
            }
        }

        public List<C_client> SelectAllClients([FromBody] C_client client)
        {

            List<C_client> clientList = new List<C_client>();

            DataTable dataTable= BD.getData($"select * from Client");

            clientList = (from DataRow dataRow in dataTable.Rows
                          select new C_client()
                          {
                              id = Convert.ToInt32(dataRow["id"]),
                              name = Convert.ToString(dataRow["name"]),
                              last_name = Convert.ToString(dataRow["last_name"]),
                              bill_id = Convert.ToInt32(dataRow["bill_id"])
                          }).ToList();

            return clientList;
        } 

        public List<C_client> SelectOneClient(int id)
        {
            List<C_client> clientList = new List<C_client>();

            DataTable dataTable = BD.getData($"select * from Client where id= {id}");

            clientList = (from DataRow dataRow in dataTable.Rows
                          select new C_client()
                          {
                              id = Convert.ToInt32(dataRow["id"]),
                              name = Convert.ToString(dataRow["name"]),
                              last_name = Convert.ToString(dataRow["last_name"]),
                              bill_id = Convert.ToInt32(dataRow["bill_id"])
                          }).ToList();

            return clientList;
        }

        public List<C_bill_client> SelectBill(int id)
        {
            List<C_bill> invoiceDetaill = new List<C_bill>();
            List<C_bill_client> invoiceList = new List<C_bill_client>();

            DataTable BillClientList = BD.getData($"select * from client_bill where id = {id}");
            DataTable Bill = BD.getData($"select * from bill where client_bill_id = {id}");

            invoiceDetaill = (from DataRow dataRows in Bill.Rows
                              select new C_bill()
                              {
                                  id = Convert.ToInt32(dataRows["id"]),
                                  client_bill_id = Convert.ToInt32(dataRows["client_bill_id"]),
                                  description = dataRows["description"].ToString(),
                                  value = Convert.ToInt32(dataRows["value"])
                              }).ToList();

            invoiceList = (from DataRow dataRow in BillClientList.Rows
                           select new C_bill_client()
                           {
                               Id = Convert.ToInt32(dataRow["id"]),
                               client_id = Convert.ToInt32(dataRow["client_id"]),
                               cod = Convert.ToInt32(dataRow["cod"]),
                               bill_details = invoiceDetaill
                           }).ToList();

            return invoiceList;

            //List<C_bill_client> BillClientList = new List<C_bill_client>();
            //List<C_bill> Bill = new List<C_bill>();

            //DataTable dataTable = BD.getData($"select * from client_bill where id= {userId}");
            //DataTable dataTableDetails = BD.getData($"select * from bill where client_bill_id = {userId}");

            //Bill = (from DataRow datarow in dataTableDetails.Rows
            //        select new C_bill()
            //        {
            //            id = Convert.ToInt32(datarow["id"]),
            //            client_bill_id = Convert.ToInt32(datarow["client_bill_id"]),
            //            description = (datarow["description"]).ToString(),
            //            value = Convert.ToInt32(datarow["value"])
            //        }).ToList();


            //BillClientList = (from DataRow dataRow in dataTable.Rows
            //                  select new C_bill_client()
            //                  {
            //                      Id = Convert.ToInt32(dataRow["id"]),
            //                      client_id = Convert.ToInt32(dataRow["client_id"]),
            //                      cod = Convert.ToInt32(dataRow["cod"]),
            //                      bill_details = Bill
            //                  }).ToList();

            //return BillClientList;
        }
    }
}
