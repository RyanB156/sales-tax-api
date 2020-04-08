using System;
using System.Collections.Generic;

namespace SalesTaxCalculator
{

    /// <summary>
    /// Used to fetch the sales tax rate for counties in North Carolina.
    /// </summary>
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
                { "alamance county","6.75" },
                { "alexander county","7.00" },
                { "alleghany county","6.75" },
                { "anson county","7.00" },
                { "ashe county","7.00" },
                { "avery county","6.75" },
                { "beaufort county","6.75" },
                { "bertie county","6.75" },
                { "bladen county","6.75" },
                { "brunswick county","6.75" },
                { "buncombe county","7.00" },
                { "burke county","6.75" },
                { "cabarrus county","7.00" },
                { "caldwell county","6.75" },
                { "camden county","6.75" },
                { "carteret county","7.00" },
                { "caswell county","6.75" },
                { "catawba county","7.00" },
                { "chatham county","7.00" },
                { "cherokee county","7.00" },
                { "chowan county","6.75" },
                { "clay county","7.00" },
                { "cleveland county","6.75" },
                { "columbus county","6.75" },
                { "craven county","6.75" },
                { "cumberland county","7.00" },
                { "currituck county","6.75" },
                { "dare county","6.75" },
                { "davidson county","7.00" },
                { "davie county","6.75" },
                { "duplin county","7.00" },
                { "durham county","7.50" },
                { "edgecombe county","7.00" },
                { "forsyth county","6.75" },
                { "franklin county","6.75" },
                { "gaston county","7.00" },
                { "gates county","6.75" },
                { "graham county","7.00" },
                { "granville county","6.75" },
                { "greene county","7.00" },
                { "guilford county","6.75" },
                { "halifax county","7.00" },
                { "harnett county","7.00" },
                { "haywood county","7.00" },
                { "henderson county","6.75" },
                { "hertford county","7.00" },
                { "hoke county","6.75" },
                { "hyde county","6.75" },
                { "iredell county","6.75" },
                { "jackson county","7.00" },
                { "johnston county","6.75" },
                { "jones county","7.00" },
                { "lee county","7.00" },
                { "lenoir county","7.00" },
                { "lincoln county","7.00" },
                { "macon county","6.75" },
                { "madison county","6.75" },
                { "martin county","7.00" },
                { "mcdowell county","6.75" },
                { "mecklenburg county","7.25" },
                { "mitchell county","6.75" },
                { "montgomery county","7.00" },
                { "moore county","7.00" },
                { "nash county","7.00" },
                { "new hanover county","7.00" },
                { "northampton county","6.75" },
                { "onslow county","7.00" },
                { "orange county","7.50" },
                { "pamlico county","6.75" },
                { "pasquotank county","7.00" },
                { "pender county","6.75" },
                { "perquimans county","6.75" },
                { "person county","7.50" },
                { "pitt county","7.00" },
                { "polk county","6.75" },
                { "randolph county","7.00" },
                { "richmond county","6.75" },
                { "robeson county","7.00" },
                { "rockingham county","7.00" },
                { "rowan county","7.00" },
                { "rutherford county","7.00" },
                { "sampson county","7.00" },
                { "scotland county","6.75" },
                { "stanly county","7.00" },
                { "stokes county","6.75" },
                { "surry county","7.00" },
                { "swain county","7.00" },
                { "transylvania county","6.75" },
                { "tyrrell county","6.75" },
                { "union county","6.75" },
                { "vance county","6.75" },
                { "wake county","7.25" },
                { "warren county","7.00" },
                { "washington county","6.75" },
                { "watauga county","6.75" },
                { "wayne county","6.75" },
                { "wilkes county","7.00" },
                { "wilson county","7.00" },
                { "yadkin county","6.75" },
                { "yancey county","6.75" },
            };
        }

        /// <summary>
        /// Attempts to fetch the sales tax rate that corresponds to the specified county. Returns the tax rate or -1 if the county does not exist.
        /// </summary>
        /// <param name="countyName">The county to fetch the tax rate for</param>
        /// <returns>A double representing the sales tax rate in that county or -1 if the county was not found</returns>
        public decimal GetTaxAmount(string countyName)
        {
            if (countyName == null)
                return -1;
            try
            {
                Decimal.TryParse(taxData[countyName], out decimal result);
                return result;
            }
            catch (KeyNotFoundException)
            {
                return -1;
            }
        }

    }
}
