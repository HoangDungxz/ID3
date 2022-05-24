using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ID3.Models;
using UnityEngine;

namespace ID3.Controllers
{
    public class HomeController : Controller
    {
        private Weather_Focast db = new Weather_Focast();


        // GET: Home
        public ActionResult Index()
        {
            List<Prediction> predictions = db.Predictions.ToList();

            float entropy = tinhEntropy(predictions);

            float gain_humididities = tinhGain<Humididity>(predictions, db.Humididities.ToList(), entropy);
            float gain_precipitations = tinhGain<Precipitation>(predictions, db.Precipitations.ToList(), entropy);
            float gain_temperatures = tinhGain<Temperature>(predictions, db.Temperatures.ToList(), entropy);
            float gain_winds = tinhGain<Wind>(predictions, db.Winds.ToList(), entropy);
   
            return View();
        }


        public ActionResult Data()
        {
            var predictions = db.Predictions.Include(p => p.Humididity).Include(p => p.Precipitation).Include(p => p.Temperature).Include(p => p.Weather).Include(p => p.Wind);

            var predictionsList = predictions.ToArray();

            return View(predictions.ToList());
        }

        [HttpPost]
        public ActionResult Index(PredictionData data)
        {

            return View();
        }


        private float tinhEntropy(List<Prediction> predictions)
        {
            float sum = predictions.Count();

            float solandem_drizzle = predictions.Where(x => x.Weather.Name.Contains("drizzle")).Count();
            float tansuat_drizzle = solandem_drizzle / sum;

            float solandem_rain = predictions.Where(x => x.Weather.Name.Contains("rain")).Count();
            float tansuat_rain = solandem_rain / sum;

            float solandem_sun = predictions.Where(x => x.Weather.Name.Contains("sun")).Count();
            float tansuat_sun = solandem_sun / sum;

            float solandem_fog = predictions.Where(x => x.Weather.Name.Contains("fog")).Count();
            float tansuat_fog = solandem_fog / sum;

            float solandem_snow = predictions.Where(x => x.Weather.Name.Contains("snow")).Count();
            float tansuat_snow = solandem_snow / sum;

            var entropy =
                       -tansuat_drizzle * Mathf.Log(tansuat_drizzle) +
                        tansuat_rain * Mathf.Log(tansuat_rain) +
                        tansuat_sun * Mathf.Log(tansuat_sun) +
                        tansuat_fog * Mathf.Log(tansuat_fog) +
                        tansuat_snow * Mathf.Log(tansuat_snow);


            return -entropy;
        }

        private float tinhGain<T>(List<Prediction> predictions, List<T> atributes, float entropy) where T : GenericTable
        {
            float sum = db.Predictions.Count();
            List<Tyle> tyles = new List<Tyle>();

            float tich = 0;
            foreach (GenericTable atribute in atributes)
            {
                int countElement = db.Predictions.Where(x => x.Precipitation.Id == atribute.Id).Count();

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

    }

    //private Attribute GetBestAttribute(List<List<string>> Examples, List<Attribute> Attributes, string bestat)
    //{
    //    double MaxGain = Gain(Examples, Attributes[0], bestat);
    //    int Max = 0;
    //    for (int i = 1; i < Attributes.Count; i++)
    //    {
    //        double GainCurrent = Gain(Examples, Attributes[i], bestat);
    //        if (MaxGain < GainCurrent)
    //        {
    //            MaxGain = GainCurrent;
    //            Max = i;
    //        }
    //    }
    //    return Attributes[Max];
    //}

    class Tyle
    {
        public int precipitationId;
        public int countElement;
        public float tanxuat;

        public Tyle(int precipitationId, int countElement, float tanxuat)
        {
            this.precipitationId = precipitationId;
            this.countElement = countElement;
            this.tanxuat = tanxuat;
        }

    }
}
