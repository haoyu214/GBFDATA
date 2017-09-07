    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    namespace GreeterClient
    {
        public partial class STAT_SERVICE_DATA:EntityBase
        {

            [StringLength(50)]
            public string PLATFORM_CODE { get; set; }

            public DateTime? STAT_DATE { get; set; }

            public int? ONLINE_WB { get; set; }

            public double? WB_RATE { get; set; }

            public int? ONLINE_FJ { get; set; }

            public int? ALL_FJ { get; set; }

            public double? FJ_RATE { get; set; }

            public int? ONLINE_WIFI { get; set; }

            [Column("ALL_ WIFI")]
            public int? ALL__WIFI { get; set; }

            [Column("WIFI _RATE")]
            public double? WIFI__RATE { get; set; }

            public int? ONLINE_KK { get; set; }

            [Column("ALL_ KK")]
            public int? ALL__KK { get; set; }

            public double? KK_RATE { get; set; }

            public int? CORRECT_SERVICE { get; set; }

            public double? CORRECT_RATE { get; set; }

            [StringLength(50)]
            public string ISEC_VERSION { get; set; }

            [StringLength(50)]
            public string DB_VERSION { get; set; }

            public int? UPLOAD_STATUS { get; set; }
        }
    }

