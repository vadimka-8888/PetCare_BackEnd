namespace PetCareDB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Illness
    {
        public int IllnessId { get; set; }

        public int PetId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBegining { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfEnding { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
