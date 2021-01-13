using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class GlobalVariables
    {
        public enum Categories
        {
            Small,      // For Rooms with area less than 35 m^2
            Medium,     // For Rooms with are between 36 to 50 m^2
            Large       // For Rooms with area between 51 to 100 m^2
        }

        public enum BookingStatus
        {
            Optional,
            Definitive,
            Cancelled,
            Deleted
        }
    }
}
