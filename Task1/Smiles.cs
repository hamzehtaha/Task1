using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Survey;
using System.Windows.Forms;

namespace Task1
{
    class Smiles :Qustions 
    {
        public Smiles(int Id,int IdForType, string NewText, string TypeOfQuestion, int Order,int NumberOfSmiles) {
            try
            {
                this.NewText = NewText;
                this.NumberOfSmiles = NumberOfSmiles;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.Id = Id;
                this.IdForType = IdForType;
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        public Smiles(int idForType, string NewText, string TypeOfQuestion, int Order, int NumberOfSmiles)
        {
            try
            {
                this.NewText = NewText;
                this.NumberOfSmiles = NumberOfSmiles;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.IdForType = idForType;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }

        }
        public Smiles() { }
        public int NumberOfSmiles { get; set; }
        public int IdForType { get; set; }

    }
}
