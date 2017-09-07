
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    namespace GreeterClient
    {
        public partial class STAT_INTERFACE_STAT:EntityBase
        {


            [StringLength(50)]
            public string PLATFORM_CODE { get; set; }

            public DateTime? STAT_DATE { get; set; }

            [StringLength(50)]
            public string INTERFACE_TYPE { get; set; }

            [StringLength(50)]
            public string TROUBLE_REASON { get; set; }

            public int? UPLOAD_STATUS { get; set; }
        }
    }

