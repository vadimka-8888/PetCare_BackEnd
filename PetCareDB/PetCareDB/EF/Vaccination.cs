namespace PetCareDB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vaccination
    {
        public int VaccinationId { get; set; }

        public int PetId { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Column(TypeName = "image")]
        public byte[] OfficialDocument { get; set; }

        public bool? NecessityOfRevaccination { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
