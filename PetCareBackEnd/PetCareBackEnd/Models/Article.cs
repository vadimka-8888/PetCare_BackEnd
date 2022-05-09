namespace PetCareBackEnd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    public partial class Article
    {
        public int ArticleId { get; set; }

        [Required]
        [StringLength(20)]
        public string Animal { get; set; }

        [Required]
        public string TextOfArticle { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public bool IsFavourite { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public string ImageAdress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favourite> Favourites { get; set; }
    }
}
