using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using LDLR.Core.Domain;
namespace GreeterClient
{
 [Table("tCompany")]
    public  class Company: EntityBase
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
        public string AreaCode { get; set; }
        public string Address { get; set; }
        public string CreateTime { get; set; }


    }
}
