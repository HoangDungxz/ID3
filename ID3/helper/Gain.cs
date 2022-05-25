using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ID3.Models;
using UnityEngine;

namespace ID3.helper
{
    public class Gain
    {
        private List<Prediction> predictions;

        public Gain(List<Prediction> _predictions)
        {
            this.predictions = _predictions;
        }

        public float tinhGain(List<Precipitation> atributes, float entropy)
        {
            float sum = predictions.Count();
            List<Tyle> tyles = new List<Tyle>();

            float tich = 0;
            foreach (Precipitation atribute in atributes)
            {
                int countElement = predictions.Where(x => x.Precipitation.Id == atribute.Id).Count();

                float tanxuat = countElement / sum;

                Tyle tyle = new Tyle(atribute.Id, countElement, tanxuat);

                tyles.Add(tyle);

                float solandem_drizzle = predictions.Where(x => x.Precipitation.Id == atribute.Id && x.Weather.Name.Contains("drizzle")).Count();
                float tansuat_drizzle = solandem_drizzle / sum;

                float solandem_rain = predictions.Where(x => x.Precipitation.Id == atribute.Id && x.Weather.Name.Contains("rain")).Count();
                float tansuat_rain = solandem_rain / sum;

                float solandem_sun = predictions.Where(x => x.Precipitation.Id == atribute.Id && x.Weather.Name.Contains("sun")).Count();
                float tansuat_sun = solandem_sun / sum;

                float solandem_fog = predictions.Where(x => x.Precipitation.Id == atribute.Id && x.Weather.Name.Contains("fog")).Count();
                float tansuat_fog = solandem_fog / sum;

                float solandem_snow = predictions.Where(x => x.Precipitation.Id == atribute.Id && x.Weather.Name.Contains("snow")).Count();
                float tansuat_snow = solandem_snow / sum;

                float entropy_con =
                    tansuat_drizzle != 0 ? tansuat_drizzle * Mathf.Log(tansuat_drizzle) : 0 +
                    tansuat_drizzle != 0 ? tansuat_rain * Mathf.Log(tansuat_rain) : 0 +
                    tansuat_drizzle != 0 ? tansuat_sun * Mathf.Log(tansuat_sun) : 0 +
                    tansuat_drizzle != 0 ? tansuat_fog * Mathf.Log(tansuat_fog) : 0 +
                    tansuat_drizzle != 0 ? tansuat_snow * Mathf.Log(tansuat_snow) : 0;
                tich += (tanxuat * -entropy_con);

            }
            float gain = entropy - tich;

            return gain;
        }

        public float tinhGain(List<Humididity> atributes, float entropy)
        {
            float sum = predictions.Count();
            List<Tyle> tyles = new List<Tyle>();

            float tich = 0;
            foreach (Humididity atribute in atributes)
            {
                int countElement = predictions.Where(x => x.Humididity.Id == atribute.Id).Count();

                float tanxuat = countElement / sum;

                Tyle tyle = new Tyle(atribute.Id, countElement, tanxuat);

                tyles.Add(tyle);

                float solandem_drizzle = predictions.Where(x => x.Humididity.Id == atribute.Id && x.Weather.Name.Contains("drizzle")).Count();
                float tansuat_drizzle = solandem_drizzle / sum;

                float solandem_rain = predictions.Where(x => x.Humididity.Id == atribute.Id && x.Weather.Name.Contains("rain")).Count();
                float tansuat_rain = solandem_rain / sum;

                float solandem_sun = predictions.Where(x => x.Humididity.Id == atribute.Id && x.Weather.Name.Contains("sun")).Count();
                float tansuat_sun = solandem_sun / sum;

                float solandem_fog = predictions.Where(x => x.Humididity.Id == atribute.Id && x.Weather.Name.Contains("fog")).Count();
                float tansuat_fog = solandem_fog / sum;

                float solandem_snow = predictions.Where(x => x.Humididity.Id == atribute.Id && x.Weather.Name.Contains("snow")).Count();
                float tansuat_snow = solandem_snow / sum;

                float entropy_con =
                    tansuat_drizzle != 0 ? tansuat_drizzle * Mathf.Log(tansuat_drizzle) : 0 +
                    tansuat_drizzle != 0 ? tansuat_rain * Mathf.Log(tansuat_rain) : 0 +
                    tansuat_drizzle != 0 ? tansuat_sun * Mathf.Log(tansuat_sun) : 0 +
                    tansuat_drizzle != 0 ? tansuat_fog * Mathf.Log(tansuat_fog) : 0 +
                    tansuat_drizzle != 0 ? tansuat_snow * Mathf.Log(tansuat_snow) : 0;
                tich += (tanxuat * -entropy_con);

            }
            float gain = entropy - tich;

            return gain;
        }

        public float tinhGain(List<Temperature> atributes, float entropy)
        {
            float sum = predictions.Count();
            List<Tyle> tyles = new List<Tyle>();

            float tich = 0;
            foreach (Temperature atribute in atributes)
            {
                int countElement = predictions.Where(x => x.Temperature.Id == atribute.Id).Count();

                float tanxuat = countElement / sum;

                Tyle tyle = new Tyle(atribute.Id, countElement, tanxuat);

                tyles.Add(tyle);

                float solandem_drizzle = predictions.Where(x => x.Temperature.Id == atribute.Id && x.Weather.Name.Contains("drizzle")).Count();
                float tansuat_drizzle = solandem_drizzle / sum;

                float solandem_rain = predictions.Where(x => x.Temperature.Id == atribute.Id && x.Weather.Name.Contains("rain")).Count();
                float tansuat_rain = solandem_rain / sum;

                float solandem_sun = predictions.Where(x => x.Temperature.Id == atribute.Id && x.Weather.Name.Contains("sun")).Count();
                float tansuat_sun = solandem_sun / sum;

                float solandem_fog = predictions.Where(x => x.Temperature.Id == atribute.Id && x.Weather.Name.Contains("fog")).Count();
                float tansuat_fog = solandem_fog / sum;

                float solandem_snow = predictions.Where(x => x.Temperature.Id == atribute.Id && x.Weather.Name.Contains("snow")).Count();
                float tansuat_snow = solandem_snow / sum;

                float entropy_con =
                    tansuat_drizzle != 0 ? tansuat_drizzle * Mathf.Log(tansuat_drizzle) : 0 +
                    tansuat_drizzle != 0 ? tansuat_rain * Mathf.Log(tansuat_rain) : 0 +
                    tansuat_drizzle != 0 ? tansuat_sun * Mathf.Log(tansuat_sun) : 0 +
                    tansuat_drizzle != 0 ? tansuat_fog * Mathf.Log(tansuat_fog) : 0 +
                    tansuat_drizzle != 0 ? tansuat_snow * Mathf.Log(tansuat_snow) : 0;
                tich += (tanxuat * -entropy_con);

            }
            float gain = entropy - tich;

            return gain;
        }

        public float tinhGain(List<Wind> atributes, float entropy)
        {
            float sum = predictions.Count();
            List<Tyle> tyles = new List<Tyle>();

            float tich = 0;
            foreach (Wind atribute in atributes)
            {
                int countElement = predictions.Where(x => x.Wind.Id == atribute.Id).Count();

                float tanxuat = countElement / sum;

                Tyle tyle = new Tyle(atribute.Id, countElement, tanxuat);

                tyles.Add(tyle);

                float solandem_drizzle = predictions.Where(x => x.Wind.Id == atribute.Id && x.Weather.Name.Contains("drizzle")).Count();
                float tansuat_drizzle = solandem_drizzle / sum;

                float solandem_rain = predictions.Where(x => x.Wind.Id == atribute.Id && x.Weather.Name.Contains("rain")).Count();
                float tansuat_rain = solandem_rain / sum;

                float solandem_sun = predictions.Where(x => x.Wind.Id == atribute.Id && x.Weather.Name.Contains("sun")).Count();
                float tansuat_sun = solandem_sun / sum;

                float solandem_fog = predictions.Where(x => x.Wind.Id == atribute.Id && x.Weather.Name.Contains("fog")).Count();
                float tansuat_fog = solandem_fog / sum;

                float solandem_snow = predictions.Where(x => x.Wind.Id == atribute.Id && x.Weather.Name.Contains("snow")).Count();
                float tansuat_snow = solandem_snow / sum;

                float entropy_con =
                    tansuat_drizzle != 0 ? tansuat_drizzle * Mathf.Log(tansuat_drizzle) : 0 +
                    tansuat_drizzle != 0 ? tansuat_rain * Mathf.Log(tansuat_rain) : 0 +
                    tansuat_drizzle != 0 ? tansuat_sun * Mathf.Log(tansuat_sun) : 0 +
                    tansuat_drizzle != 0 ? tansuat_fog * Mathf.Log(tansuat_fog) : 0 +
                    tansuat_drizzle != 0 ? tansuat_snow * Mathf.Log(tansuat_snow) : 0;
                tich += (tanxuat * -entropy_con);

            }
            float gain = entropy - tich;

            return gain;
        }
    }

}