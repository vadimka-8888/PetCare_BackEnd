using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_DataBase
{
    public class Animal
    {
        //атрибуты
        public int AnimalId { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }

        //внешние ключи и навигационные свойства
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Disease> Diseases { get; set; }
        public List<Inoculation> Inoculations { get; set; }
    }
}
