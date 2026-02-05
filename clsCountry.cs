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
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static string GetCountryName(int CountryID)
        {
            return clsCountryData.GetCountryName(CountryID);
        }
    }
}
