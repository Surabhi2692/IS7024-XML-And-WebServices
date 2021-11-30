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
                    moviesData = webClient.DownloadString("https://xmlprojectis20211128205504.azurewebsites.net/Top250Movies");
                    var moviesRecommendations = MoviesRecommendations.FromJson(moviesData);

                    if (moviesRecommendations != null)
                    {
                        if (!string.IsNullOrWhiteSpace(query))
                        {
                            var moviesRecommendationList = moviesRecommendations.ToList();

                            var titleBasedMovies = moviesRecommendationList.Where(x => x.FullTitle.ToUpper().Contains(query.ToUpper())).ToList();
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
                            ViewData["MoviesRecommendations"] = moviesRecommendations.ToList();
                        }
                    }
                    else
                    {
                        ViewData["MoviesRecommendations"] = null;
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
