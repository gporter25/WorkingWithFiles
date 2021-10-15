using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary
{
    public static class GeoExtensions
    {
        /// <summary>
        /// Validate sender is a valid latitude
        /// </summary>
        /// <param name="value">double value to validate</param>
        /// <returns>true if valid, false if not valid</returns>
        public static bool IsLatitude(this double value) 
            => value  <= 90.0 &&  value >= -90.0;

        /// <summary>
        /// Validate sender is a valid longitude
        /// </summary>
        /// <param name="value">double value to validate</param>
        /// <returns>true if valid, false if not valid</returns>
        public static bool IsLongitude(this double value) 
            => value  <= 180.0 && value >= -180.0;
    }
}
