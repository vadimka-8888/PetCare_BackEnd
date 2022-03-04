namespace PetCareDB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Note
    {
        public int NoteId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(300)]
        public string TextOfNote { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}
