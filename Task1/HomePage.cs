using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Collections;
using System.Runtime.InteropServices;
using System.Configuration; 
namespace Task1
{
    public partial class HomePage : MetroFramework.Forms.MetroForm
    {
        private int ID = -1;
        private string Type;
        
        public HomePage()
        { 
            
            InitializeComponent();
            Slider.ShowQuestion();
            Smiles.ShowQuestion();
            Stars.ShowQuestion();
            Qustions.GetData(dataGridView1); 
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // to go to the Add page 
            QuestionsInformation f = new QuestionsInformation(dataGridView1,-1,"",1); 
          //  FQuorm2 f = new Form2(dataGridView1);
            f.Show();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }
  
        private void metroButton1_Click(object sender, EventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this function for delete the answer 
            List<int> del = new List<int>();
            Slider obj1 = null;
            Stars obj3 = null;
            Smiles obj2 = null; 
            foreach (   DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)dataGridView1.Rows[row.Index].Cells[0].Value == true)
                {
                    string s = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                    for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                        if (s.Equals(Qustions.lissSlid.ElementAt(i).Qustion))
                            del.Add(Qustions.lissSlid.ElementAt(i).Id); 
                }
            }
            if (del.Count == 0)
            {
                MessageBox.Show("This is not select any Question !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else { 
            DialogResult dialogResult = MessageBox.Show("Are you sure to Delete this answer ?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes && del.Count > 0)
                {
                    for (int j = 0; j < del.Count; ++j)
                    {
                        for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                        {
                            if (Qustions.lissSlid.ElementAt(i).Id == del.ElementAt(j))
                            {
                                if (Qustions.lissSlid.ElementAt(i) is Slider)
                                {
                                    obj1 = (Slider)Qustions.lissSlid.ElementAt(i);
                                    obj1.Delete(obj1.Id, obj1.IdForType);
                                    Qustions.lissSlid.Remove(obj1);
                                    break;
                                }
                                else if (Qustions.lissSlid.ElementAt(i) is Smiles)
                                {
                                    obj2 = (Smiles)Qustions.lissSlid.ElementAt(i);
                                    obj2.Delete(obj2.Id, obj2.idForType);
                                    Qustions.lissSlid.Remove(obj2);
                                    break;
                                    
                                }
                                else if (Qustions.lissSlid.ElementAt(i) is Stars)
                                {
                                    obj3 = (Stars)Qustions.lissSlid.ElementAt(i);
                                    obj3.Delete(obj3.Id, obj3.idForType); 
                                    Qustions.lissSlid.RemoveAt(i);
                                    break;
                                    
                                }
                            }
                        }

                    }
                    Qustions.GetData(dataGridView1); 
                    MessageBox.Show("Your Questions is deleted !");
                }
            }
            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            List<int> del = new List<int>();
            List<string> types = new List<string>(); 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)dataGridView1.Rows[row.Index].Cells[0].Value == true)
                {
                    string s = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                    for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                        if (s.Equals(Qustions.lissSlid.ElementAt(i).Qustion))
                        {
                            del.Add(Qustions.lissSlid.ElementAt(i).Id);
                            types.Add(Qustions.lissSlid.ElementAt(i).TypeOfQuestion); 
                        }
                }
            }
            if (del.Count > 1)
            {
                MessageBox.Show("You Can not choose more than one Question for Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (del.Count == 0)
            {
                MessageBox.Show("Please Choose one Question for Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (del.Count == 1)
            {
                QuestionsInformation f = new QuestionsInformation(dataGridView1, del.ElementAt(0), types.ElementAt(0),2);
                f.Show();
            }
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
    }
}
