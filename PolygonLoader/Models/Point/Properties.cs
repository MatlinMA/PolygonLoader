using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonLoader
{
    public partial class Point
    {
        public double Longitude
        {
            get
            { 
                return f_longitude; 
            }
            set
            {
                if ((value < -90) || (value > 90))
                {
                    f_latitude = 0;
                    f_longitude = 0;
                    IsRight = false;
                }
                else
                    f_longitude = value;

                OnPropertyChanged();
            }
        }

        public double Latitude
        {
            get
            {
                return f_latitude;
            }
            set
            {
                if ((value < -180) || (value > 180))
                {
                    f_latitude = 0;
                    f_longitude = 0;
                    IsRight = false;
                }
                else
                    f_latitude = value;

                OnPropertyChanged();
            }
        }

        public bool IsRight
        {
            get
            {
                return f_isRight;
            }

            set
            {
                f_isRight = value;

                OnPropertyChanged();
            }
        }
    }
}
