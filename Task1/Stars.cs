using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Stars : Qustions
    {
        private int numberOfStars;
        public int NumberOfStars { get; set; }

        public Stars(int Id,string Qustion,string TypeOfQuestion, int order , int NumberOfStars) {
            this.Qustion = Qustion;
            this.NumberOfStars = NumberOfStars;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id; 
        }
        public Stars(string Qustion, string TypeOfQuestion, int order, int NumberOfStars)
        {
            this.Qustion = Qustion;
            this.NumberOfStars = NumberOfStars;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;

        }
    }
}
