using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode { get; private set; }

        public int UserID { get; private set; }
        public int PersonID { get; private set; }
        private clsPerson _Person; 

        public clsPerson Person
        {
            get => _Person; 
            set
            {
                _Person = value; 

                if (value != null)
                {
                    this.PersonID = value.PersonID;
                }
            }
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
            this.Person = new clsPerson();

            Mode = enMode.AddNew;
        }


        // New overload that sets UserID so instances returned from Find have correct ID
        private clsUser(int UserID, int PersonID, string UserName,
            string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this.Person = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            if (!_Person.Save()) // Ensure the person is saved and has a valid PersonID
            {
                return false;
            }
            this.PersonID = _Person.PersonID;

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName,
                this.Password, this.IsActive);

            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
            if (!_Person.Save()) // Ensure the person is saved and has a valid PersonID
            {
                return false;
            }
            this.PersonID = _Person.PersonID;

            return clsUserData.UpdateUser(this.UserID, this.PersonID, 
                this.UserName, this.Password, this.IsActive);
        }

        public static bool Delete(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;


            bool found = clsUserData.GetUserInfoByID(UserID, ref PersonID,
                ref UserName, ref Password, ref IsActive);

            if (found)
            {
                return new clsUser(UserID, PersonID, UserName,
                    Password, IsActive);
                    
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            bool found = clsUserData.GetUserInfoByPersonID(PersonID, ref UserID,
                ref UserName, ref Password, ref IsActive);

            if (found)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName)
        {
            int UserID = -1;
            int PersonID = -1;
            string Password = string.Empty;
            bool IsActive = false;


            bool found = clsUserData.GetUserInfoByUserName(UserName,
                ref UserID, ref PersonID, ref Password, ref IsActive);

            if (found)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;

            bool found = clsUserData.GetUserInfoByUsernameAndPassword(UserName,
                Password, ref UserID, ref PersonID, ref IsActive);
            if (found)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static bool IsUserExists(int UserID)
        {
            return clsUserData.IsUserExists(UserID);
        }

        public static bool IsUserExists(string UserName)
        {
            return clsUserData.IsUserExists(UserName);
        }

        public static bool IsUserExistsForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        Mode = enMode.Update;
                        return _AddNewUser();
                    }
                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }
}
