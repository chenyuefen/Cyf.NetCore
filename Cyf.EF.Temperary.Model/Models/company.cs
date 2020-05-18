namespace Cyf.EF.Temperary.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cyf_datas.company")]
    public partial class company
    {
        [Key]
        public int company_id { get; set; }

        [StringLength(255)]
        public string company_name { get; set; }

        [StringLength(255)]
        public string company_position { get; set; }
    }
}
