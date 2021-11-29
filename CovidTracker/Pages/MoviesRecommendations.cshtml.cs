using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CovidTracker.Pages
{
    public class MoviesRecommendationsModel : PageModel
    {
        public string SearchTerm { get; set; }
        public void OnGet(string query)
        {
            using (var webClient = new WebClient())
            {
                string moviesData = string.Empty;

                try
                {
                    moviesData = webClient.DownloadString("https://imdb-api.com/en/API/Top250Movies/k_emiixec6");
                    var moviesRecommendations = MoviesRecommendations.FromJson(moviesData);

                    if (!string.IsNullOrWhiteSpace(query))
                    {
                        var moviesRecommendationList = moviesRecommendations.Items.ToList();

                        var titleBasedMovies = moviesRecommendationList.Where(x => x.FullTitle.Contains(query)).ToList();
                        if (titleBasedMovies != null && titleBasedMovies.Count > 0)
                        {
                            ViewData["MoviesRecommendations"] = titleBasedMovies;
                        }
                        else
                        {
                            ViewData["MoviesRecommendations"] = null;
                        }
                    }
                    else
                    {
                        ViewData["MoviesRecommendations"] = moviesRecommendations.Items.ToList();
                    }
                    SearchTerm = query;

                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while calling API", ex);
                }
            }
        }

    }
    
}
