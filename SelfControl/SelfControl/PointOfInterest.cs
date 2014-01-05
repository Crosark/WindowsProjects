using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfControl
{
    public class PointOfInterest
    {
        public string address { get; set; }
        public string name { get; set; }
        public GeoCoordinate coord { get; set; }
        public int rating { get; set; }
        public string price { get; set; }

        public PointOfInterest(string _add, string _name, int _rating, string _price, GeoCoordinate _coord)
        {
            address = _add;
            name = _name;
            coord = _coord;
            rating = _rating;
            price = _price;
        }
    }
}
