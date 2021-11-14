using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CovidTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json");

                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText("StockSchema.json"));
                IList<string> validationEvents = new List<string>();
                JObject jsonObject = JObject.Parse(jsonString);
                TrendingStock result = new TrendingStock();
                if (jsonObject.IsValid(schema, out validationEvents))
                {
                    result = TrendingStock.FromJson(jsonString);
                }
                else
                {
                    //error handling
                }



                string jsonStringVaccineData = webClient.DownloadString("https://data.cdc.gov/resource/rh2h-3yt2.json");
                var covid19CasesAndDeaths = CasesAndDeaths.Covid19CasesAndDeaths.FromJson(jsonString);
                var covid19Vaccine = Vaccines.Covid19Vaccine.FromJson(jsonStringVaccineData);
                ViewData["Covid19CasesAndDeaths"] = covid19CasesAndDeaths;
                ViewData["Covid19Vaccines"] = covid19Vaccine;

            
            }
         
        }
    }
}
