using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CovidTracker.Pages
{
    public class Covid19VaccinesModel : PageModel
    {
        public void OnGet()
        {
            using (var webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://data.cdc.gov/resource/rh2h-3yt2.json");
                var covid19Vaccines = Vaccines.Covid19Vaccine.FromJson(jsonString);
                ViewData["Covid19Vaccines"] = covid19Vaccines;
            }
        }
    }
}
