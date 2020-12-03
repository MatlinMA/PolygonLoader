using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonLoader
{
    public partial class PolygonLoaderViewModel
    {
        public List<Point> PointsList
        {
            get
            { 
                return f_pointsList; 
            }
            set
            {
                if (f_pointsList != value)
                {
                    f_pointsList = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
