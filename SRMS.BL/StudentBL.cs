using System;
using System.Collections.Generic;
using System.Data;
using SRMS.BO;
using SRMS.DL;

namespace SRMS.BL
{
    public class StudentBL
    {
        #region GetStudentDetails Method

        public static DataTable GetStudentDetails()
        {
            DataTable dtable = new DataTable();

            dtable = StudentDL.FetchStudentDetails();

            return dtable;
        }

        #endregion

        #region GetRollNumber Method

        public static List<string> GetRollNumber()
        {
            List<string> lstRollNumber = new List<string>();

            lstRollNumber = StudentDL.FetchRollNumber();

            return lstRollNumber;
        }

        #endregion

        #region UpdateStudentDetails Method

        public static int UpdateStudentDetails(StudentBO objStudentBO)
        {
            int result = StudentDL.UpdateStudentDetails(objStudentBO);

            return result;
        }

        #endregion

        #region AddNewStudentDetails Method

        public static int AddNewStudentDetails(StudentBO objStudentBO)
        {
            int result = StudentDL.AddNewStudentDetails(objStudentBO);

            return result;
        }

        #endregion

        #region RemoveStudent Method

        public static int RemoveStudent(Int32 iStudentId)
        {
            int result = StudentDL.DeleteStudentDetails(iStudentId);

            return result;
        }

        #endregion
    }
}