using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_DataBase
{
    public class Disease
    {
        //атрибуты
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //внешние ключи и навигационные свойства
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
