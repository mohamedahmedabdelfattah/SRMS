using System;

namespace SRMS.BO
{
    public class StudentBO
    {
        #region Constructor

        public StudentBO()
        {
        }

        #endregion

        #region Variable Declaration

        private Int32 iStudentId = 0;
        private string sRollNumber = string.Empty;
        private string sFirstName = string.Empty;
        private string sLastName = string.Empty;
        private string sGender = string.Empty;
        private string sStream = string.Empty;

        #endregion

        #region Properties

        public Int32 StudentId
        {
            get
            {
                return iStudentId;
            }
            set
            {
                iStudentId = value;
            }
        }

        public string RollNumber
        {
            get
            {
                return sRollNumber;
            }
            set
            {
                sRollNumber = value;
            }
        }
        public string FirstName
        {
            get
            {
                return sFirstName;
            }
            set
            {
                sFirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return sLastName;
            }
            set
            {
                sLastName = value;
            }
        }

        public string Gender
        {
            get
            {
                return sGender;
            }
            set
            {
                sGender = value;
            }
        }

        public string Stream
        {
            get
            {
                return sStream;
            }
            set
            {
                sStream = value;
            }
        }

        #endregion
    }
}