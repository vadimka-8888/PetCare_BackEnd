using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetCareBackEnd.AuxiliaryClasses
{
    public class OnlyArticle
    {
        public int ArticleId { get; set; }
        public string Animal { get; set; }
        public byte[] Image { get; set; }
        public string ImageAdress { get; set; }
        public bool IsFavourite { get; set; }
        public string TextOfArticle { get; set; }
        public string Title { get; set; }

        public OnlyArticle(int id, string a, byte[] im, string path, bool fav, string text, string title)
        {
            ArticleId = id;
            Animal = a;
            Image = im;
            ImageAdress = path;
            IsFavourite = fav;
            TextOfArticle = text;
            Title = title;
        }
    }
}
