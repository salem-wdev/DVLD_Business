using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsCountry
    {

        public int CountryID { get; }
        public string CountryName { get; }

        public clsCountry()
        {
            // Private constructor to prevent instantiation without parameters
            CountryID = -1;
            CountryName = string.Empty;
        }

        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = string.Empty;
            bool IsFound = clsCountryData.GetCountryByID(CountryID, ref CountryName);
            if (IsFound)
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = 0;
            bool IsFound = clsCountryData.GetCountryByCountryName(CountryName, ref CountryID);
            if (IsFound)
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }
    }
}
