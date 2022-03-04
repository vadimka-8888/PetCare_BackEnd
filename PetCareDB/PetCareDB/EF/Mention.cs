namespace PetCareDB.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mention
    {
        public int MentionId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string TextOfMention { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan? Time { get; set; }

        public virtual User User { get; set; }
    }
}
