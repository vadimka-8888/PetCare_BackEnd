using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_DataBase
{
    public class Inoculation
    {
        //атрибуты
        public int InoculationId { get; set; }
        public string Name { get; set; }

        //внешние ключи и навигационные свойства
        public int AnomalId { get; set; }
        public Animal Animal { get; set; }
    }
}
