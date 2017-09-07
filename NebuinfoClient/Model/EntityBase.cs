using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GreeterClient
{
  public abstract  class EntityBase

    {
        [DisplayName("编号"),Key,Required,Column("Id")]
        public int ID { get; set; }
    }
}
