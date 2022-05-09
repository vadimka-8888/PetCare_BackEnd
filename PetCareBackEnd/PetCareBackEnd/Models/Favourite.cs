
namespace PetCareBackEnd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Favourite
    {
        public int FavouriteId { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public virtual User User { get; set; }

        public virtual Article Article { get; set; }
    }
}
