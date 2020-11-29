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
using System.Diagnostics;
using Task1;

namespace Survey
{
    public partial class QuestionsInformation : Form
    {
        /// <summary>
        /// privtae objects for add,edit and delete 
        /// </summary>
        private Qustions QuestionWillDeleteOrEdit = null;
        private Slider SliderForEdit = null;
        private Stars StarForEdit = null;
        private Smiles SmileForEdit = null;
        public Qustions ReturnNewQuestion { get; set; }
        /// <summary>
        /// This constructor for hide and if i choose edit will show the variable for types of question
        /// </summary>
        public QuestionsInformation(Qustions QuestionWillDeleteOrEdit, string AddOrEdit)
        {
            InitializeComponent();
            InitHide();
            this.QuestionWillDeleteOrEdit = QuestionWillDeleteOrEdit;
            NewText.Focus();
            try
            {
                if (TypeOfChoice.Edit.ToString() == AddOrEdit)
                {
                    GroupOfTypes.Visible = false;
                    StaticObjects.AddOrEdit = TypeOfChoice.Edit.ToString();
                    ShowDataForEdit();
                }
                else if (TypeOfChoice.Add.ToString() == AddOrEdit)
                {
                    GroupOfTypes.Visible = true;
                    StaticObjects.AddOrEdit = TypeOfChoice.Add.ToString();
                    InitHide();
                }
                if (QuestionWillDeleteOrEdit != null)
                {
                    if (TypeOfQuestion.Slider.ToString() == QuestionWillDeleteOrEdit.TypeOfQuestion)
                        ShowForSlider();
                    else if (TypeOfQuestion.Smily.ToString() == QuestionWillDeleteOrEdit.TypeOfQuestion)
                        ShowForSmiles();
                    else if (TypeOfQuestion.Stars.ToString() == QuestionWillDeleteOrEdit.TypeOfQuestion)
                        ShowForStars();
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// this when choose the slider the slider attrubites will appear
        /// </summary>
        private void ShowForSlider()
        {
            try
            {
                InitHide();
                GroupOfSlider.Visible = true; 
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// this when choose the smile the smile attrubites will appear
        /// </summary>
        private void ShowForSmiles()
        {
            try
            {
                InitHide();
                panel2.Visible = true;
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// this when choose the stars the stars attrubites will appear
        /// </summary>
        private void ShowForStars()
        {
            try
            {
                InitHide();
                   panel1.Visible = true;
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// This old data in object and appear when press edit question 
        /// </summary>
        private void ShowDataForEdit()
        {
            try
            {
                GroupOfTypes.Visible = true;
                GroupOfTypes.Enabled = false;
                if (TypeOfQuestion.Slider.ToString() == QuestionWillDeleteOrEdit.TypeOfQuestion)
                {
                    Slider EditSlider = (Slider)QuestionWillDeleteOrEdit;
                    NewText.Text = EditSlider.NewText;
                    NewOrder.Value = EditSlider.Order;
                    NewStartValue.Value = EditSlider.StartValue;
                    NewEndValue.Value = EditSlider.EndValue;
                    NewStartValueCaption.Text = EditSlider.StartCaption;
                    NewEndValueCaption.Text = EditSlider.EndCaption;
                    SliderRadio.Checked = true;
                    SliderForEdit = (Slider)QuestionWillDeleteOrEdit;
                }
                else if (TypeOfQuestion.Stars.ToString() == QuestionWillDeleteOrEdit.TypeOfQuestion)
                {
                    Stars EditStar = (Stars)QuestionWillDeleteOrEdit;
                    NewText.Text = EditStar.NewText;
                    NewOrder.Value = EditStar.Order;
                    NewNumberOfStars.Value = EditStar.NumberOfStars;
                    StarsRadio.Checked = true;
                    StarForEdit = (Stars)QuestionWillDeleteOrEdit;
                }
                else if (TypeOfQuestion.Smily.ToString() == QuestionWillDeleteOrEdit.TypeOfQuestion)
                {
                    Smiles EditSmile = (Smiles)QuestionWillDeleteOrEdit;
                    NewText.Text = EditSmile.NewText;
                    NewOrder.Value = EditSmile.Order;
                    NewNumberOfSmiles.Value = EditSmile.NumberOfSmiles;
                    SmilyRadio.Checked = true;
                    SmileForEdit = (Smiles)QuestionWillDeleteOrEdit;
                }
            }catch(Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// This For Hide panel and radio Button
        /// </summary>
        private void InitHide()
        {
            
            try
            {
                GroupOfSlider.Visible = false ;
                panel2.Visible = false;
                panel1.Visible = false; 
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }

        }
        /// <summary>
        /// to check the string is number or not ?
        /// </summary>
        private bool IsNumber(string Number)
        {
            
            return int.TryParse(Number, out int N);

        }
        private void questionDetalis1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///  // for radio Button cahnges 
        /// </summary>
        private void Slider_CheckedChange(object sender, EventArgs e)
        {
           
            try
            {
                if (SliderRadio.Checked == true)
                {
                    ShowForSlider();
                }
            } catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// for radio Button cahnges 
        /// </summary>
        private void Smily_CheckedChange(object sender, EventArgs e)
        {
            
            try
            {
                if (SmilyRadio.Checked == true)
                {
                    ShowForSmiles(); 
                }
            } catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// for radio Button cahnges
        /// </summary>
        private void Stars_CheckedChange(object sender, EventArgs e)
        {
            try
            {
                if (StarsRadio.Checked == true)
                {
                    ShowForStars(); 

                }
            } catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// This Function For User know is data is edited or Added
        /// </summary>
        private void DataEnter()
        {
            try
            {
                MessageBox.Show(Survey.Properties.Resource1.DataIsEnterd);
                this.Close();
            }catch(Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }

        }
        /// <summary>
        /// This Function to Check validation of data 
        /// </summary>
        private bool CheckTheData( Qustions TypeQuestion)
        {
            try
            {
                if (NewText.Text == Constant.Empty)
                {
                    MessageBox.Show(Survey.Properties.Resource1.QuestionIsEmptyMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    return false;
                }
                else if (IsNumber(NewText.Text))
                {
                    MessageBox.Show(Survey.Properties.Resource1.QuestionIsJustANumberMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                else if (NewOrder.Value <= 0)
                {
                    MessageBox.Show(Survey.Properties.Resource1.NewOrderLessThanZeroMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
               
                if (TypeQuestion is Slider)
                {
                    if (NewStartValue.Value <= 0)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.StartValueLessThanZeroMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValue.Value <= 0)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.EndValueLessThanZeroMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValue.Value > 100)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.StartValueGreaterThanOneHundredMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValue.Value > 100)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.EndValueGreaterThanOneHundredMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValue.Value >= NewEndValue.Value)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.TheEndValueSholudGreaterThanStartValueMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewStartValueCaption.Text == Constant.Empty)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.StartCaptionEmptyMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (IsNumber(NewStartValueCaption.Text))
                    {
                        MessageBox.Show(Survey.Properties.Resource1.StartCaptionJustNumberMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (NewEndValueCaption.Text == Constant.Empty)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.EndCaptionEmptyMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (IsNumber(NewEndValueCaption.Text))
                    {
                        MessageBox.Show(Survey.Properties.Resource1.EndCaptionJustNumberMessage, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (TypeQuestion is Smiles)
                {
                    if (NewNumberOfSmiles.Value <= 1 || NewNumberOfSmiles.Value > 5)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.NumberOfSmileBetweenFiveAndTow, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if (TypeQuestion is Stars)
                {
                    if (NewNumberOfStars.Value <= 0 || NewNumberOfStars.Value > 10)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.NumberOfStrasBetweenTenAndOne, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);

                return false; 
            }
            return true; 
        }
        /// <summary>
        /// when i press save button go to this function and from AddOrEdit var will know i edit or add the question 
        /// </summary>
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (StaticObjects.AddOrEdit == TypeOfChoice.Add.ToString())
                {
                    if (SliderRadio.Checked)
                    {
                        Slider NewQuestion = new Slider(); 
                        if (CheckTheData(NewQuestion))
                        {
                            NewQuestion.NewText = NewText.Text;
                            NewQuestion.Order = Convert.ToInt32(NewOrder.Value);
                            NewQuestion.TypeOfQuestion = TypeOfQuestion.Slider.ToString();
                            NewQuestion.StartValue = Convert.ToInt32(NewStartValue.Text);
                            NewQuestion.EndValue = Convert.ToInt32(NewEndValue.Text);
                            NewQuestion.StartCaption  = NewStartValueCaption.Text;
                            NewQuestion.EndCaption = NewEndValueCaption.Text;
                            ReturnNewQuestion = (Slider)DataBaseConnections.AddQuestion(NewQuestion);
                            if (StaticObjects.SuccOfFail == 1)
                                DataEnter();
                        }
                    }
                    else if (SmilyRadio.Checked)
                    {

                        Smiles NewQuestion = new Smiles(); 
                        if (CheckTheData(NewQuestion))
                        {
                            NewQuestion.NewText = NewText.Text;
                            NewQuestion.Order = Convert.ToInt32(NewOrder.Value);
                            NewQuestion.TypeOfQuestion = TypeOfQuestion.Smily.ToString();
                            NewQuestion.NumberOfSmiles = Convert.ToInt32(NewNumberOfSmiles.Text);
                            ReturnNewQuestion = (Smiles)DataBaseConnections.AddQuestion(NewQuestion);
                            if (StaticObjects.SuccOfFail == 1)
                                DataEnter();
                        }
                    }
                    else if (StarsRadio.Checked)
                    {
                        Stars NewQuestion = new Stars(); 
                        if (CheckTheData(NewQuestion))
                        {
                            NewQuestion.NewText = NewText.Text;
                            NewQuestion.Order = Convert.ToInt32(NewOrder.Value);
                            NewQuestion.TypeOfQuestion = TypeOfQuestion.Stars.ToString();
                            NewQuestion.NumberOfStars = Convert.ToInt32(NewNumberOfStars.Text);
                            ReturnNewQuestion = (Stars)DataBaseConnections.AddQuestion(NewQuestion);
                            if (StaticObjects.SuccOfFail ==1)
                                DataEnter();
                        }
                    }else
                    {
                        MessageBox.Show(Survey.Properties.Resource1.NotChooseTheType, Constant.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
            try { 
                 if (StaticObjects.AddOrEdit == TypeOfChoice.Edit.ToString())
                 {
                   if (SliderForEdit != null)
                   {
                        SliderForEdit.NewText = NewText.Text;
                        SliderForEdit.Order = Convert.ToInt32(NewOrder.Value);
                        SliderForEdit.StartValue = Convert.ToInt32(NewStartValue.Value);
                        SliderForEdit.EndValue = Convert.ToInt32(NewEndValue.Value);
                        SliderForEdit.StartCaption = NewStartValueCaption.Text;
                        SliderForEdit.EndCaption = NewEndValueCaption.Text;
                        if (CheckTheData(SliderForEdit))
                        {

                            ReturnNewQuestion=DataBaseConnections.EditQuestion(SliderForEdit);
                            MessageBox.Show(Properties.Resource1.TheEditMessage);
                            StaticObjects.SuccOfFail = 1; 
                            this.Close();

                        }

                   }else if (SmileForEdit != null)
                    {
                        SmileForEdit.NewText = NewText.Text;
                        SmileForEdit.Order = Convert.ToInt32(NewOrder.Value);
                        SmileForEdit.NumberOfSmiles = Convert.ToInt32(NewNumberOfSmiles.Value);
                        if (CheckTheData(SmileForEdit))
                        {
                            ReturnNewQuestion=DataBaseConnections.EditQuestion(SmileForEdit);
                            MessageBox.Show(Properties.Resource1.TheEditMessage);
                            StaticObjects.SuccOfFail = 1;
                            this.Close();
                        }
                    }else if (StarForEdit != null)
                    {
                        StarForEdit.NewText = NewText.Text;
                        StarForEdit.Order = Convert.ToInt32(NewOrder.Value);
                        StarForEdit.NumberOfStars = Convert.ToInt32(NewNumberOfStars.Value);
                        if (CheckTheData(StarForEdit))
                        {
                            ReturnNewQuestion=DataBaseConnections.EditQuestion(StarForEdit);
                            MessageBox.Show(Properties.Resource1.TheEditMessage);
                            StaticObjects.SuccOfFail = 1;
                            this.Close();
                        }
                    }
                     
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
            }
        }
        /// <summary>
        /// for close this page
        /// </summary>
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
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void QuestionsInformation_Load(object sender, EventArgs e)
        {

        }
    }
}
