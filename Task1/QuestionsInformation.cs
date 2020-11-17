using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Survey;
using System.Threading;

namespace Task1
{
    public partial class QuestionsInformation : Form
    {
        public DataGridView ListOfQuestion;
        
        private void ShowForSlider()
        {
            try
            {
                InitHide();
                GroupOfSlider.Visible = true; 
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void ShowForSmiles()
        {
            try
            {
                InitHide();
                panel2.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void ShowForStars()
        {
            try
            {
                InitHide();
                   panel1.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        public QuestionsInformation(DataGridView ListOfQuestion, int Id, string Type, string AddOrEdit)
        {
            InitializeComponent();
            this.ListOfQuestion = ListOfQuestion;
            Constant.Id = Id;
            Constant.Type = Type;
            StaticObjects.NewSlider = null;
            StaticObjects.NewSmile = null;
            StaticObjects.NewStars = null;
            NewText.Focus(); 
            try
            {
                if (AddOrEdit.Equals(Constant.EDIT))
                {
                    GroupOfTypes.Visible = false;
                    Constant.AddOrEdit = Constant.EDIT; 
                    GetObject(); 

                }
                else if (AddOrEdit.Equals(Constant.ADD))
                {
                    GroupOfTypes.Visible = true;
                    Constant.AddOrEdit = Constant.ADD;
                    InitHide(); 
                }
                if (Type.Equals(Constant.SliderString))
                {
                    ShowForSlider(); 
                }
                else if (Type.Equals(Constant.SmilyString))
                {
                    ShowForSmiles(); 
                }
                else if (Type.Equals(Constant.StarsString))
                {
                    ShowForStars(); 

                }
            }catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void InitHide()
        {
            // This For Hide panel and radio Button
            try
            {
                GroupOfSlider.Visible = false ;
                panel2.Visible = false;
                panel1.Visible = false; 
            }catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }

        }
        private bool IsNumber(string Number)
        {
            // to check the string is number or not ?
            
            return int.TryParse(Number, out int N);

        }
        private void QuestionsInformation_Load(object sender, EventArgs e)
        {

        }
        private void questionDetalis1_Load(object sender, EventArgs e)
        {

        }
        private void Slider_CheckedChange(object sender, EventArgs e)
        {
            // for radio Button cahnges 
            try
            {
                if (SliderRadio.Checked == true)
                {
                    ShowForSlider();
                }
            } catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void Smily_CheckedChange(object sender, EventArgs e)
        {
            // for radio Button cahnges 
            try
            {
                if (SmilyRadio.Checked == true)
                {
                    ShowForSmiles(); 
                }
            } catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void Stars_CheckedChange(object sender, EventArgs e)
        {
            // for radio Button cahnges
            try
            {
                if (StarsRadio.Checked == true)
                {
                    ShowForStars(); 

                }
            } catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void GetObject()
        {
            // This Function To get Object Selected And Show data in my Edit page 
            try
            {
                for (int i = 0; i < StaticObjects.ListOfAllQuestion.Count; ++i)
                {
                    if (StaticObjects.ListOfAllQuestion.ElementAt(i).Id == Constant.Id)
                    {
                        if (StaticObjects.ListOfAllQuestion.ElementAt(i) is Slider)
                        {
                            StaticObjects.NewSlider = (Slider)StaticObjects.ListOfAllQuestion.ElementAt(i);
                            NewText.Text = StaticObjects.NewSlider.NewText;
                            NewOrder.Value = StaticObjects.NewSlider.Order;
                            NewStartValue.Value = StaticObjects.NewSlider.StartValue;
                            NewEndValue.Value = StaticObjects.NewSlider.EndValue;
                            NewStartValueCaption.Text = StaticObjects.NewSlider.StartCaption;
                            NewEndValueCaption.Text = StaticObjects.NewSlider.EndCaption;
                        }
                        else if (StaticObjects.ListOfAllQuestion.ElementAt(i) is Smiles)
                        {
                            StaticObjects.NewSmile = (Smiles)StaticObjects.ListOfAllQuestion.ElementAt(i);
                            NewText.Text = StaticObjects.NewSmile.NewText;
                            NewOrder.Value = StaticObjects.NewSmile.Order;
                            NewNumberOfSmiles.Value = StaticObjects.NewSmile.NumberOfSmiles;
                        }else
                        {
                            StaticObjects.NewStars = (Stars)StaticObjects.ListOfAllQuestion.ElementAt(i);
                            NewText.Text = StaticObjects.NewStars.NewText;
                            NewOrder.Value = StaticObjects.NewStars.Order;
                            NewNumberOfStars.Value = StaticObjects.NewStars.NumberOfStars;
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void DataEnter()
        {
            // This Function For User know is data is edited or Added
            try
            {
                MessageBox.Show(Constant.DataIsEnterd);
                Qustions.GetData(ListOfQuestion);
                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }

        }
        private bool CheckTheData( Qustions TypeQuestion)
        {
            // This Function to Check validation of data 
            try
            {
                if (NewText.Text == Constant.Empty)
                {
                    MessageBox.Show( Constant.QuestionIsEmptyMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    return false;
                }
                else if (IsNumber(NewText.Text))
                {
                    MessageBox.Show(Constant.QuestionIsJustANumberMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                else if (NewOrder.Value <= 0)
                {
                    MessageBox.Show(Constant.NewOrderLessThanZeroMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
               
                if (TypeQuestion is Slider)
                {
                    if (NewStartValue.Value <= 0)
                    {
                        MessageBox.Show(Constant.StartValueLessThanZeroMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValue.Value <= 0)
                    {
                        MessageBox.Show(Constant.EndValueLessThanZeroMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValue.Value > 100)
                    {
                        MessageBox.Show(Constant.StartValueGreaterThanOneHundredMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValue.Value > 100)
                    {
                        MessageBox.Show(Constant.EndValueGreaterThanOneHundredMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValue.Value >= NewEndValue.Value)
                    {
                        MessageBox.Show(Constant.TheEndValueSholudGreaterThanStartValueMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValueCaption.Text == Constant.Empty)
                    {
                        MessageBox.Show(Constant.StartCaptionEmptyMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (IsNumber(NewStartValueCaption.Text))
                    {
                        MessageBox.Show(Constant.StartCaptionJustNumberMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValueCaption.Text == Constant.Empty)
                    {
                        MessageBox.Show(Constant.EndCaptionEmptyMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (IsNumber(NewEndValueCaption.Text))
                    {
                        MessageBox.Show(Constant.EndCaptionJustNumberMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (TypeQuestion is Smiles)
                {
                    if (NewNumberOfSmiles.Value <= 1 || NewNumberOfSmiles.Value > 5)
                    {
                        MessageBox.Show(Constant.NumberOfSmileBetweenFiveAndTow, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (TypeQuestion is Stars)
                {
                    if (NewNumberOfStars.Value <= 0 || NewNumberOfStars.Value > 10)
                    {
                        MessageBox.Show(Constant.NumberOfStrasBetweenTenAndOne, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);

                return false; 
            }
            return true; 
        }
        private void Save_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                if (Constant.AddOrEdit == Constant.ADD)
                {
                    
                    Qustions NewQuestion = null;
                    
                    bool f = false;
                    if (SliderRadio.Checked)
                    {
                        

                        NewQuestion = StaticObjects.QuestionFactory.GetQustion(Constant.SliderString);
                        
                        f = CheckTheData(NewQuestion);
                        if (f)
                        {
                            
                            StaticObjects.Attrubite = new string[7];
                            StaticObjects.Attrubite[0] = NewText.Text;
                            StaticObjects.Attrubite[1] = NewOrder.Text;
                            StaticObjects.Attrubite[2] = Constant.SliderString;
                            StaticObjects.Attrubite[3] = NewStartValue.Text;
                            StaticObjects.Attrubite[4] = NewEndValue.Text;
                            StaticObjects.Attrubite[5] = NewStartValueCaption.Text;
                            StaticObjects.Attrubite[6] = NewEndValueCaption.Text;
                            

                        }
                    }
                    else if (SmilyRadio.Checked)
                    {

                        NewQuestion = StaticObjects.QuestionFactory.GetQustion(Constant.SmilyString);
                        f = CheckTheData(NewQuestion);
                        if (f)
                        {
                            StaticObjects.Attrubite = new string[4];
                            StaticObjects.Attrubite[0] = NewText.Text;
                            StaticObjects.Attrubite[1] = NewOrder.Text;
                            StaticObjects.Attrubite[2] = Constant.SmilyString;
                            StaticObjects.Attrubite[3] = NewNumberOfSmiles.Text;





                        }
                    }
                    else if (StarsRadio.Checked)
                    {
                        NewQuestion = StaticObjects.QuestionFactory.GetQustion(Constant.StarsString);
                        f = CheckTheData(NewQuestion);
                        if (f)
                        {
                            StaticObjects.Attrubite = new string[4];
                            StaticObjects.Attrubite[0] = NewText.Text;
                            StaticObjects.Attrubite[1] = NewOrder.Text;
                            StaticObjects.Attrubite[2] = Constant.StarsString;
                            StaticObjects.Attrubite[3] = NewNumberOfStars.Text;
                        }
                    }else
                    {
                        MessageBox.Show(Constant.NotChooseTheType, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (f)
                    {
                       DataBaseConnections.AddQuestion(NewQuestion, StaticObjects.Attrubite);  
                        if (Constant.DoneOrNot.Equals(Constant.Done))
                        DataEnter(); 
                        
                    }
                }
            }catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
            try { 
                 if (Constant.AddOrEdit == Constant.EDIT)
                 {
                    
                    if (StaticObjects.NewSlider != null)
                    {
                        if (CheckTheData(StaticObjects.NewSlider))
                        {
                            StaticObjects.Attrubite = new string[8];
                            StaticObjects.Attrubite[0] = NewText.Text;
                            StaticObjects.Attrubite[1] = NewOrder.Value + Constant.Empty;
                            StaticObjects.Attrubite[2] = NewStartValue.Value + Constant.Empty;
                            StaticObjects.Attrubite[3] = NewEndValue.Value + Constant.Empty;
                            StaticObjects.Attrubite[4] = NewStartValueCaption.Text;
                            StaticObjects.Attrubite[5] = NewEndValueCaption.Text;
                            StaticObjects.Attrubite[6] = StaticObjects.NewSlider.IdForType + Constant.Empty;
                            StaticObjects.Attrubite[7] = StaticObjects.NewSlider.Id + Constant.Empty;
                            StaticObjects.NewSlider.NewText = StaticObjects.Attrubite[0];
                            StaticObjects.NewSlider.Order = Convert.ToInt32(StaticObjects.Attrubite[1]);
                            StaticObjects.NewSlider.StartValue = Convert.ToInt32(StaticObjects.Attrubite[2]);
                            StaticObjects.NewSlider.EndValue = Convert.ToInt32(StaticObjects.Attrubite[3]);
                            StaticObjects.NewSlider.StartCaption = StaticObjects.Attrubite[4];
                            StaticObjects.NewSlider.EndCaption = StaticObjects.Attrubite[5];
                            DataBaseConnections.EditQuestion(StaticObjects.NewSlider, StaticObjects.Attrubite); 
                            DataEnter();
                        }
                        
                    }
                    else if (StaticObjects.NewSmile != null)
                    {
                        if (CheckTheData(StaticObjects.NewSmile))
                        {
                            StaticObjects.Attrubite = new string[5];
                            StaticObjects.Attrubite[0] = NewText.Text;
                            StaticObjects.Attrubite[1] = NewOrder.Value + Constant.Empty;
                            StaticObjects.Attrubite[2] = NewNumberOfSmiles.Value + Constant.Empty;
                            StaticObjects.Attrubite[3] = StaticObjects.NewSmile.IdForType + Constant.Empty;
                            StaticObjects.Attrubite[4] = StaticObjects.NewSmile.Id + Constant.Empty;
                            StaticObjects.NewSmile.NewText = StaticObjects.Attrubite[0];
                            StaticObjects.NewSmile.Order = Convert.ToInt32(StaticObjects.Attrubite[1]);
                            StaticObjects.NewSmile.NumberOfSmiles = Convert.ToInt32(StaticObjects.Attrubite[2]);
                            DataBaseConnections.EditQuestion(StaticObjects.NewSmile, StaticObjects.Attrubite);
                            DataEnter();

                        }
                    }
                    else if (StaticObjects.NewStars != null)
                    {
                        if (CheckTheData(StaticObjects.NewStars))
                        {
                            StaticObjects.Attrubite = new string[5];
                            StaticObjects.Attrubite[0] = NewText.Text;
                            StaticObjects.Attrubite[1] = NewOrder.Value + Constant.Empty;
                            StaticObjects.Attrubite[2] = NewNumberOfStars.Value + Constant.Empty;
                            StaticObjects.Attrubite[3] = StaticObjects.NewStars.IdForType + Constant.Empty;
                            StaticObjects.Attrubite[4] = StaticObjects.NewStars.Id + Constant.Empty;
                            StaticObjects.NewStars.NewText = StaticObjects.Attrubite[0];
                            StaticObjects.NewStars.Order = Convert.ToInt32(StaticObjects.Attrubite[1]);
                            StaticObjects.NewStars.NumberOfStars = Convert.ToInt32(StaticObjects.Attrubite[2]);
                            DataBaseConnections.EditQuestion(StaticObjects.NewStars, StaticObjects.Attrubite);
                            DataEnter();
                        }
                    }
                }

            }

            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NewNumberOfSmiles_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
