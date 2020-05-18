namespace Cyf.EF.MYSQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("company")]
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
