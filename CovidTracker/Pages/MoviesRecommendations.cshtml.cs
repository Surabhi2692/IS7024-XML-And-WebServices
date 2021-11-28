using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CovidTracker.Pages
{
    public class MoviesRecommendationsModel : PageModel
    {
        public void OnGet(string query)
        {
            using (var webClient = new WebClient())
            {
                string moviesData = string.Empty;

                try
                {
                    moviesData = webClient.DownloadString("https://raw.githubusercontent.com/Anukriti007/XMLProjectIS/master/csvjson(2).json");
                    var moviesRecommendations = MoviesRecommendations.FromJson(moviesData);
                    ViewData["MoviesRecommendations"] = moviesRecommendations;
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while calling API", ex);
                }
            }
        }
    }
}
