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
        //This abstract Method And override in all types of Question 
        public static void GetData(DataGridView ListViewQuestion)
        {
            //This Function For get data and Show it in my datagridview 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlDataAdapter  SqlAdapter = new SqlDataAdapter(Constant.SelectStarFromQuestion, Connection);

            try
            {
                Connection.Open();
                DataTable DataTableView = new DataTable();
                SqlAdapter.Fill(DataTableView);
                ListViewQuestion.Rows.Clear();
                foreach (DataRow item in DataTableView.Rows)
                {
                    int Index = ListViewQuestion.Rows.Add();
                    ListViewQuestion.Rows[Index].Cells[0].Value = item[Constant.Qustions_textString].ToString();
                    ListViewQuestion.Rows[Index].Cells[1].Value = item[Constant.Type_Of_QustionString].ToString();
                    ListViewQuestion.Rows[Index].Cells[2].Value = item[Constant.Qustion_orderString].ToString();

                }
                // View the data in datagridview 
            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

        }
        
    }
}
