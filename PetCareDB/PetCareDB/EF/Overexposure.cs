namespace PetCareDB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Overexposure
    {
        public int OverexposureId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Animal { get; set; }

        [StringLength(300)]
        public string ONote { get; set; }

        public int? Cost { get; set; }

        public virtual User User { get; set; }
    }
}
