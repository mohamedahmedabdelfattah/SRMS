using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SRMS.BO;

namespace SRMS.DL
{
    public class StudentDL
    {
        #region Constructor

        public StudentDL()
        {
        }

        #endregion

        #region Variable Declaration

        private static SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SRMSDB;" + "Integrated Security=True");

        // Initiate SqlDataAdapter with select command and connection
        private static SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Student_Table", conn);

        #endregion

        #region FetchStudentDetails Method

        public static DataTable FetchStudentDetails()
        {
            try
            {
                conn.Open();

                DataTable dtable = new DataTable();

                adp.Fill(dtable);

                return dtable;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        
        #endregion

        #region Fetch RollNumber Details

        public static List<string> FetchRollNumber()
        {
            List<string> lstRollNumber = new List<string>();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT RollNumber From Student_Table", conn);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    lstRollNumber.Add(rdr["RollNumber"].ToString());
                }

                return lstRollNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        
        #endregion

        #region UpdateStudentDetails Method
        
        public static int UpdateStudentDetails(StudentBO objStudentBO)
        {

            try
            {
                Int32 iStudentId = objStudentBO.StudentId;
                string sRollno = objStudentBO.RollNumber;
                string sFName = objStudentBO.FirstName;
                string sLName = objStudentBO.LastName;
                string sGender = objStudentBO.Gender;
                string sStream = objStudentBO.Stream;

                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Student_Table SET RollNumber = @RollNo, FirstName = @FName, LastName = @LName," +
                    "Gender = @Gender, Stream = @Stream WHERE StudentId = @Id", conn);

                cmd.Parameters.Add(new SqlParameter("@Id", iStudentId));
                cmd.Parameters.Add(new SqlParameter("@RollNo", sRollno));
                cmd.Parameters.Add(new SqlParameter("@FName", sFName));
                cmd.Parameters.Add(new SqlParameter("@LName", sLName));
                cmd.Parameters.Add(new SqlParameter("@Gender", sGender));
                cmd.Parameters.Add(new SqlParameter("@Stream", sStream));

                int result = cmd.ExecuteNonQuery();

                conn.Close();

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        
        #endregion

        #region AddNewStudentDetails Method
        
        public static int AddNewStudentDetails(StudentBO objStudentBO)
        {
            try
            {
                string sRollno = objStudentBO.RollNumber;
                string sFName = objStudentBO.FirstName;
                string sLName = objStudentBO.LastName;
                string sGender = objStudentBO.Gender;
                string sStream = objStudentBO.Stream;

                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Student_Table(RollNumber, FirstName, LastName, Gender,  Stream)"
                + "VALUES (@RollNo, @FName, @LName, @Gender, @Stream)", conn);

                cmd.Parameters.Add(new SqlParameter("@RollNo", sRollno));
                cmd.Parameters.Add(new SqlParameter("@FName", sFName));
                cmd.Parameters.Add(new SqlParameter("@LName", sLName));
                cmd.Parameters.Add(new SqlParameter("@Gender", sGender));
                cmd.Parameters.Add(new SqlParameter("@Stream", sStream));

                int result = cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        
        #endregion

        #region DeleteStudentDetails Method
        
        public static int DeleteStudentDetails(Int32 iStudentId)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Student_Table WHERE StudentId = @Id", conn);

                cmd.Parameters.Add(new SqlParameter("@Id", iStudentId));

                int result = cmd.ExecuteNonQuery();

                conn.Close();
                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion
    }
}