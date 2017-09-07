   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    namespace GreeterClient
    {
        public partial class STAT_DOUBLE_SYSTEM_SERVIICE:EntityBase
        {

            [StringLength(50)]
            public string PLATFORM_CODE { get; set; }

            public DateTime? STAT_DATE { get; set; }

            [StringLength(200)]
            public string SERVICE_NAME { get; set; }

            [StringLength(500)]
            public string ADDRESS { get; set; }

            public int? UPLOAD_STATUS { get; set; }
        }
    }

