using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace GreeterClient
{
    [Table("TAB_PLATFORM_INFO")]
   public class Tab_Platform_Info:EntityBase
    {
        [Column("PLATFORM_CODE")]
       
        public string Platform_Code { get; set; }
        [Column("PLATFORM_NAME")]
        public string PLATFORM_NAME { get; set; }
    }
}
