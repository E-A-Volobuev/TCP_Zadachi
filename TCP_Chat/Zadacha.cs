using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Chat
{
    public class Zadacha
    {
       
        public int Id { get; set; }
       
        public string Otdel { get; set; }
       
        public string Stadia { get; set; }
      
        public string Shifr { get; set; }
      
        public string Obj { get; set; }//объект
       
        public string Prioritet { get; set; }
       
        public DateTime Srok { get; set; }
       
        public string Avtor { get; set; }
       
        public string Ispolniteli { get; set; }
       
        public string Comment { get; set; }
       
        public bool Gotovnost { get; set; }//степень готовности

        public bool Prinyato { get; set; }
    }
}
