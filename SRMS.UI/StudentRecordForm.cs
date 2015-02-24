using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SRMS.BL;
using SRMS.BO;

namespace SRMS.UI
{
    public partial class StudentRecordForm : Form
    {
        public StudentRecordForm()
        {
            InitializeComponent();
        }

        private void showData()
        {
            DataTable dtable = StudentBL.GetStudentDetails();
            dataGridView1.DataSource = dtable;

            #region Displaying rows in different colors
            Int32 rowcount = dataGridView1.RowCount;

            for (Int32 i = 0; i < rowcount - 1; i++)
            {
                //Getting the stream value of each row
                string stream = dataGridView1.Rows[i].Cells["Stream"].Value.ToString();
                stream = stream.Trim();

                //Changing the color of the row depending on the value of Stream
                if (stream.Equals("Science"))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                else if (stream.Equals("Commerce"))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (stream.Equals("Humanities"))
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
            }//for end
            #endregion
        }

        private void StudentRecordForm_Load(object sender, EventArgs e)
        {
            showData();

            #region Making the Columns Unsortable
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            #endregion

            #region Hiding the Student Id Column
            //Hiding the Id column
            dataGridView1.Columns[0].Visible = false;
            #endregion

        }

        //Updating a Record here
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            string studentId = dataGridView1.CurrentRow.Cells["StudentId"].Value.ToString();

            if (!studentId.Equals(string.Empty))
            {
                StudentBO objStudentBO = new StudentBO();

                objStudentBO.StudentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentId"].Value);
                objStudentBO.RollNumber = dataGridView1.CurrentRow.Cells["RollNumber"].Value.ToString();
                objStudentBO.FirstName = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
                objStudentBO.LastName = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
                objStudentBO.Gender = dataGridView1.CurrentRow.Cells["Gender"].Value.ToString();
                objStudentBO.Stream = dataGridView1.CurrentRow.Cells["Stream"].Value.ToString();

                StudentBL.UpdateStudentDetails(objStudentBO);
            }
        }

        //Changing the row color as per the Stream Value
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string stream = dataGridView1.CurrentRow.Cells["Stream"].Value.ToString();
            stream = stream.Trim();

            if (stream.Equals("Science"))
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            else if (stream.Equals("Commerce"))
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.GreenYellow;
            }
            else if (stream.Equals("Humanities"))
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.OrangeRed;
            }
            else if (stream.Equals(""))
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;
            }

        }

        //Adding a New Record here
        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            string studentId = dataGridView1.CurrentRow.Cells["StudentId"].Value.ToString();

            if (studentId.Equals(string.Empty))
            {
                StudentBO objStudentBO = new StudentBO();

                try
                {
                    objStudentBO.RollNumber = dataGridView1.CurrentRow.Cells["RollNumber"].Value.ToString();
                    objStudentBO.FirstName = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
                    objStudentBO.LastName = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
                    objStudentBO.Gender = dataGridView1.CurrentRow.Cells["Gender"].Value.ToString();
                    objStudentBO.Stream = dataGridView1.CurrentRow.Cells["Stream"].Value.ToString();

                    StudentBL.AddNewStudentDetails(objStudentBO);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Deleting a Record
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Int32 iStudentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentId"].Value);

                DialogResult result1 = MessageBox.Show("Are you sure you want to delete this Student Record?",
                                      "Delete the Record",
                                       MessageBoxButtons.YesNo);

                if (result1 == DialogResult.Yes)
                {
                    int result = StudentBL.RemoveStudent(iStudentId);

                    if (result > 0)
                    {
                        //Record deleted successfully
                        //showing the new refreshed data
                        showData();
                    }
                }
            }//try
            catch
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Cannot Delete Record");
            }
        }

        //Autocomplete Code for the Gender and Stream Text Boxes
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtB = e.Control as TextBox;
            //Column Index of Gender Column is 4
            if (dataGridView1.CurrentCell.ColumnIndex == 4 && txtB != null)
            {
                txtB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtB.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtB.AutoCompleteCustomSource.AddRange(new string[] { "Male", "Female" });
            }
            //Column Index of Stream Column is 5
            else if (dataGridView1.CurrentCell.ColumnIndex == 5 && txtB != null)
            {
                txtB.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtB.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtB.AutoCompleteCustomSource.AddRange(new string[] { "Science", "Commerce", "Humanities" });
            }
        }

        //Code for validating Roll Number, First Name and Last Name
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string HeaderText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            //Roll Number can be empty but has to be unique
            if (HeaderText.Equals("RollNumber"))
            {
                //checking for any repeated Roll Number value
                Boolean flag = false;
                //Getting the Roll No entered by the user
                string rollNumUser = e.FormattedValue.ToString();

                //Getting the Roll Nos from the Database
                List<string> lstRollNumber = new List<string>();
                lstRollNumber = StudentBL.GetRollNumber();

                //if the Roll No entered by the user match with any Roll No in the DB,
                //then we set the flag to true

                foreach (string rollNumber in lstRollNumber)
                {
                    if (rollNumber.Trim().Equals(rollNumUser))
                    {
                        flag = true;
                    }
                }//foreach

                if (rollNumUser.Equals(dataGridView1.CurrentRow.Cells["RollNumber"].Value.ToString()))
                {
                    e.Cancel = false;
                }
                else if (flag == true)
                {
                    e.Cancel = true;
                    MessageBox.Show("Enter another Roll Number");
                }
            }

            if (HeaderText.Equals("FirstName"))
            {
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    e.Cancel = true;
                    MessageBox.Show("First Name Cannot Be Empty");
                }
            }

            if (HeaderText.Equals("LastName"))
            {
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    e.Cancel = true;
                    MessageBox.Show("Last Name Cannot Be Empty");
                }
            }
        }
    }
}