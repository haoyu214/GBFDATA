
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    namespace GreeterClient
    {
        public partial class STAT_OFFLINE_SERVICE:EntityBase
        {

            [StringLength(20)]
            public string PLATFORM_CODE { get; set; }

            public DateTime? STAT_DATE { get; set; }

            [StringLength(10)]
            public string SERVICE_TYPE { get; set; }

            [StringLength(14)]
            public string SERVICE_CODE { get; set; }

            [StringLength(200)]
            public string SERVICE_NAME { get; set; }

            [StringLength(500)]
            public string ADDRESS { get; set; }

            public DateTime? OFFLINE_DATE { get; set; }

            public int? UPLOAD_STATUS { get; set; }
        }
    }

