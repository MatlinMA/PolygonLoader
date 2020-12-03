using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonLoader
{
    /// <summary>
    /// Класс представляет точку полигона
    /// </summary>
    public partial class Point
    {
        private double f_latitude; //широта
        private double f_longitude; //долгота
        private bool f_isRight = true; //флаг корректности координат

        public Point(double latitude, double longitude) { Latitude = latitude; Longitude = longitude; }

        public Point() { }
    }
}
