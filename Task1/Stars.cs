using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Stars : Qustions
    {
        public int NumberOfStars { get; set; }
        public int idForType { get; set;  }
        public Stars(int Id,int idForType, string Qustion,string TypeOfQuestion, int order , int NumberOfStars) {
            this.Qustion = Qustion;
            this.NumberOfStars = NumberOfStars;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id;
            this.idForType = idForType; 
        }
        public Stars(int idForType, string Qustion, string TypeOfQuestion, int order, int NumberOfStars)
        {
            this.Qustion = Qustion;
            this.NumberOfStars = NumberOfStars;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.idForType = idForType; 

        }
    }
}
