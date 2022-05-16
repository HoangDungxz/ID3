using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
            List<Prediction> weathers = db.Predictions.ToList();

            tinhEntropy(weathers);

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


        private Double tinhEntropy(List<Prediction> predictions)
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
                        tansuat_drizzle * Mathf.Log(tansuat_drizzle) +
                        tansuat_rain * Mathf.Log(tansuat_rain) +
                        tansuat_sun * Mathf.Log(tansuat_sun) +
                        tansuat_fog * Mathf.Log(tansuat_fog) +
                        tansuat_snow * Mathf.Log(tansuat_snow);


            return entropy;
        }

    }
}
