namespace sql6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        public long ID { get; set; }

        public long Amount { get; set; }

        public long CustomerID { get; set; }
    }
}
