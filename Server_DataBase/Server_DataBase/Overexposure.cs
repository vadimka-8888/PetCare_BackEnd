using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_DataBase
{
    public class Overexposure
    {
        //атрибуты
        public int OverexposureId { get; set; }
        public string AnimalSpecies { get; set; }

        //внешние ключи и навигационные свойства
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
