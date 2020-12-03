using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonLoader
{
    public partial class OpenStreetMaps
    {
        public string Name
        {
            get
            { 
                return f_name; 
            }
            set
            {
                if (f_name != value)
                {
                    f_name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Link
        {
            get
            {
                return f_link;
            }
            set
            {
                if (f_link != value)
                {
                    f_link = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
