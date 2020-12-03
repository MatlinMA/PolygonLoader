using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PolygonLoader
{
    /// <summary>
    /// Метод возвращает коллекцию точек полигона
    /// </summary>
    public partial class OpenStreetMaps
    {
        public List<Point> GetPointsList(string address)
        {
            if (address == "")
                return null;

            var points = new List<Point>();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var json = GetJsonString(address);

            var jsonobject = JArray.Parse(json);

            if ((string)jsonobject[0]["geojson"]["type"] == "MultiPolygon")
            {
                var polygon = jsonobject[0]["geojson"]["coordinates"][0][0];

                try
                {
                    for (var i = 0; i < polygon.Count(); i++)
                    {
                        points.Add(new Point((double)jsonobject[0]["geojson"]["coordinates"][0][0][i][0], (double)jsonobject[0]["geojson"]["coordinates"][0][0][i][1]));
                    }
                }
                catch
                {
                    return null;
                }
                return points;
            }
            if ((string)jsonobject[0]["geojson"]["type"] == "Polygon")
            {
                var polygon = jsonobject[0]["geojson"]["coordinates"][0];

                try
                {
                    for (var i = 0; i < polygon.Count(); i++)
                    {
                        points.Add(new Point((double)jsonobject[0]["geojson"]["coordinates"][0][i][0], (double)jsonobject[0]["geojson"]["coordinates"][0][i][1]));
                    }
                }
                catch
                {
                    return null;
                }
                return points;
            }
            return null;
        }

        /// <summary>
        /// Метод запроса json файла с точками полигона и преобразованием его в строку
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public string GetJsonString(string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link.Replace("%useraddress%", address.Replace(" ", "%20")));
            request.Referer = "https://nominatim.openstreetmap.org";
            WebResponse response;

            try
            {
                response = request.GetResponseAsync().Result;
            }
            catch
            {
                return null;
            }

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String json = reader.ReadToEnd();
                return json;
            }
        }
    }
}
