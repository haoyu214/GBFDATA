
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
namespace GreeterClient
{
    public partial class TAB_PLATFORM_INFO : EntityBase
    {

        [StringLength(50)]
        public string PLATFORM_CODE { get; set; }

        [StringLength(500)]
        public string PLATFORM_NAME { get; set; }
    }
}
