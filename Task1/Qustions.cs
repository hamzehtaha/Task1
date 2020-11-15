using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Survey;

namespace Task1
{
   public abstract class Qustions
    {
        
        public string NewText { get; set;}
        public int Order { get; set; }
        public int Id { get; set; }
        public string TypeOfQuestion { get; set;}
        public void ShowQuestion() { }
        public abstract void AddQuestion(string [] Att);
        public abstract void EditQuestion(string[] Att);
        public static void GetData(DataGridView ListViewQuestion)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();

            SqlDataAdapter  SqlAdapter = new SqlDataAdapter("SELECT * FROM Qustions", Connection);

            try
            {
                Connection.Open();
                // SqlDataReader rd = cmd.ExecuteReader(); 
                DataTable DataTableView = new DataTable();
                //dt.Load(rd);
                SqlAdapter.Fill(DataTableView);
                ListViewQuestion.Rows.Clear();
                foreach (DataRow item in DataTableView.Rows)
                {
                    int Index = ListViewQuestion.Rows.Add();
                    ListViewQuestion.Rows[Index].Cells[0].Value = false;
                    ListViewQuestion.Rows[Index].Cells[1].Value = item[Attributes.Qustions_textString].ToString();
                    ListViewQuestion.Rows[Index].Cells[2].Value = item[Attributes.Type_Of_QustionString].ToString();
                    ListViewQuestion.Rows[Index].Cells[3].Value = item[Attributes.Qustion_orderString].ToString();

                }
                // View the data in datagridview 
            }
            catch (Exception ex)
            {

                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

        }
        public abstract void Delete(int id, int idFroType); 
    }
}
