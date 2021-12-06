using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_DataBase
{
    public class User
    {
        //атрибуты
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string District { get; set; }
        public bool CanOverexpose { get; set; }
        public double Price { get; set; }

        //внешние ключи и навигационные свойства
        public List<Animal> Animals { get; set; }
        public List<Note> Notes { get; set; }
        public List<Overexposure> Overexposures { get; set; }
    }
}
