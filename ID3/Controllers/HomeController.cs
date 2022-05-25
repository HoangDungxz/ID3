using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ID3.helper;
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





        private Attribute GetBestAttribute(List<Prediction> predictions, float entropy)
        {
            Gain gain = new Gain(predictions);
            float gain_humididities = gain.tinhGain(db.Humididities.ToList(), entropy);
            float gain_precipitations = gain.tinhGain(db.Precipitations.ToList(), entropy);
            float gain_temperatures = gain.tinhGain(db.Temperatures.ToList(), entropy);
            float gain_winds = gain.tinhGain(db.Winds.ToList(), entropy);

            float max_gain = Mathf.Max(gain_precipitations, gain_precipitations, gain_temperatures, gain_winds);


            if (max_gain == gain_humididities)
            {
                return new Attribute("Humididity");
            }
            else if (max_gain == gain_precipitations)
            {
                return new Attribute("Precipitation");
            }
            else if (max_gain == gain_temperatures)
            {
                return new Attribute("Temperature");
            }
            else if (max_gain == gain_winds)
            {
                return new Attribute("Wind");
            }
            return new Attribute("");
        }

        private TreeNode ID3(int LocationBA =  1)
        {

            int count_drizzle = db.Predictions.Where(x => x.Weather.Id == 1).Count();
            int count_rain = db.Predictions.Where(x => x.Weather.Id == 2).Count();
            int count_sun = db.Predictions.Where(x => x.Weather.Id == 3).Count();
            int count_fog = db.Predictions.Where(x => x.Weather.Id == 4).Count();
            int count_snow = db.Predictions.Where(x => x.Weather.Id == 5).Count();

            //int maxCount = Mathf.Max(count_drizzle, count_rain, count_sun, count_fog, count_snow);

            //if(maxCount == count_drizzle)
            //{
            //    return new TreeNode(new Attribute("drizzle"));
            //}
            //else if (maxCount == count_rain)
            //{
            //    return new TreeNode(new Attribute("rain"));
            //}
            //else if (maxCount == count_sun)
            //{
            //    return new TreeNode(new Attribute("sun"));
            //}
            //else if (maxCount == count_fog)
            //{
            //    return new TreeNode(new Attribute("fog"));
            //}
            //else if (maxCount == count_snow)
            //{
            //    return new TreeNode(new Attribute("snow"));
            //}

            List<Prediction> predictions = db.Predictions.ToList();

            float entropy = tinhEntropy(predictions);



            Attribute BestAttribute = GetBestAttribute(predictions, entropy);

            TreeNode Root = new TreeNode(BestAttribute);
            
           
            foreach(Prediction prediction in db.Predictions.ToList())
            {

            }


            for (int i = 0; i < BestAttribute.Value.Count; i++)
            {
                List<List<string>> Examplesvi = new List<List<string>>();
                for (int j = 0; j < Examples.Count; j++)
                {
                    if (Examples[j][LocationBA].ToString() == BestAttribute.Value[i].ToString())
                        Examplesvi.Add(Examples[j]);
                }
                if (Examplesvi.Count == 0)
                {
                    return new TreeNode(new Attribute(GetMostCommonValue(Examplesvi)));
                }
                else
                {
                    Attribute.Remove(BestAttribute);
                    Root.AddNode(ID3(LocationBA++);
                }
            }
            return Root;
        }
    }


    class TreeNode
    {
        private Attribute Attribute { get; set; }
        public TreeNode(Attribute attribute)
        {
            this.Attribute = attribute;
        }
    }

    class Attribute
    {
        public Attribute(String name)
        {
            Weather = name;
        }
        private String Weather { get; set; }
    }
}
