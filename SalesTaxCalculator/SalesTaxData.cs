using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator
{
    public class SalesTaxData
    {

        private readonly Dictionary<string, string> taxData;

        public SalesTaxData()
        {

            // A dictionary of (county,sales tax) pairs for calculating the sales tax on a retail transaction.
            // Data taken from https://www.salestaxhandbook.com/north-carolina/rates and formatted into dictionary entries
            //  using Visual Studio's Find-and-Replace tool.
            taxData = new Dictionary<string, string>()
            {
                { "Alamance County","6.75" },
                { "Alexander County","7.00" },
                { "Alleghany County","6.75" },
                { "Anson County","7.00" },
                { "Ashe County","7.00" },
                { "Avery County","6.75" },
                { "Beaufort County","6.75" },
                { "Bertie County","6.75" },
                { "Bladen County","6.75" },
                { "Brunswick County","6.75" },
                { "Buncombe County","7.00" },
                { "Burke County","6.75" },
                { "Cabarrus County","7.00" },
                { "Caldwell County","6.75" },
                { "Camden County","6.75" },
                { "Carteret County","7.00" },
                { "Caswell County","6.75" },
                { "Catawba County","7.00" },
                { "Chatham County","7.00" },
                { "Cherokee County","7.00" },
                { "Chowan County","6.75" },
                { "Clay County","7.00" },
                { "Cleveland County","6.75" },
                { "Columbus County","6.75" },
                { "Craven County","6.75" },
                { "Cumberland County","7.00" },
                { "Currituck County","6.75" },
                { "Dare County","6.75" },
                { "Davidson County","7.00" },
                { "Davie County","6.75" },
                { "Duplin County","7.00" },
                { "Durham County","7.50" },
                { "Edgecombe County","7.00" },
                { "Forsyth County","6.75" },
                { "Franklin County","6.75" },
                { "Gaston County","7.00" },
                { "Gates County","6.75" },
                { "Graham County","7.00" },
                { "Granville County","6.75" },
                { "Greene County","7.00" },
                { "Guilford County","6.75" },
                { "Halifax County","7.00" },
                { "Harnett County","7.00" },
                { "Haywood County","7.00" },
                { "Henderson County","6.75" },
                { "Hertford County","7.00" },
                { "Hoke County","6.75" },
                { "Hyde County","6.75" },
                { "Iredell County","6.75" },
                { "Jackson County","7.00" },
                { "Johnston County","6.75" },
                { "Jones County","7.00" },
                { "Lee County","7.00" },
                { "Lenoir County","7.00" },
                { "Lincoln County","7.00" },
                { "Macon County","6.75" },
                { "Madison County","6.75" },
                { "Martin County","7.00" },
                { "Mcdowell County","6.75" },
                { "Mecklenburg County","7.25" },
                { "Mitchell County","6.75" },
                { "Montgomery County","7.00" },
                { "Moore County","7.00" },
                { "Nash County","7.00" },
                { "New Hanover County","7.00" },
                { "Northampton County","6.75" },
                { "Onslow County","7.00" },
                { "Orange County","7.50" },
                { "Pamlico County","6.75" },
                { "Pasquotank County","7.00" },
                { "Pender County","6.75" },
                { "Perquimans County","6.75" },
                { "Person County","7.50" },
                { "Pitt County","7.00" },
                { "Polk County","6.75" },
                { "Randolph County","7.00" },
                { "Richmond County","6.75" },
                { "Robeson County","7.00" },
                { "Rockingham County","7.00" },
                { "Rowan County","7.00" },
                { "Rutherford County","7.00" },
                { "Sampson County","7.00" },
                { "Scotland County","6.75" },
                { "Stanly County","7.00" },
                { "Stokes County","6.75" },
                { "Surry County","7.00" },
                { "Swain County","7.00" },
                { "Transylvania County","6.75" },
                { "Tyrrell County","6.75" },
                { "Union County","6.75" },
                { "Vance County","6.75" },
                { "Wake County","7.25" },
                { "Warren County","7.00" },
                { "Washington County","6.75" },
                { "Watauga County","6.75" },
                { "Wayne County","6.75" },
                { "Wilkes County","7.00" },
                { "Wilson County","7.00" },
                { "Yadkin County","6.75" },
                { "Yancey County","6.75" },
            };
        }

        /// <summary>
        /// Attempts to fetch the sales tax rate that corresponds to the specified county. Returns the tax rate or -1 if the county does not exist.
        /// </summary>
        /// <param name="countyName">The county to fetch the tax rate for</param>
        /// <returns>A double representing the sales tax rate in that county or -1 if the county was not found</returns>
        public double GetTaxAmount(string countyName)
        {
            if (countyName == null)
                return -1;
            try
            {
                Double.TryParse(taxData[countyName], out double result);
                return result;
            }
            catch (KeyNotFoundException)
            {
                return -1;
            }
        }

    }
}
