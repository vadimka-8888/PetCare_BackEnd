using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_DataBase
{
    public class Note
    {
        //атрибуты
        public int NoteId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        //внешние ключи и навигационные свойства
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
