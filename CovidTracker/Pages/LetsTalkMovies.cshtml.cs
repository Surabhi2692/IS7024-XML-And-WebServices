using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CovidTracker.Pages
{
    public class LetsTalkMoviesModel : PageModel
    {
        
        [BindProperty]
        public FavoriteMovieForm Form { get; set; }

        public void OnGet()
        {
            ViewData["FavoriteMoviesList"] = FavoriteMovies.Movies;
        }

        public void OnPost()
        {
            FavoriteMovie movie = new FavoriteMovie()
            {
                FirstName = Form.FirstName,
                LastName = Form.LastName,
                DateOfBirth = Form.DateOfBirth,
                EmailEddress = Form.EmailAddress,
                MovieName = Form.MovieName
            };

            FavoriteMovies.Movies.Add(movie);
            ViewData["FavoriteMoviesList"] = FavoriteMovies.Movies;
        }

        public static class FavoriteMovies
        {
            public static List<FavoriteMovie> Movies { get; set; }

            static FavoriteMovies()
            {
                Movies = new List<FavoriteMovie>();
            }
        }

        public class FavoriteMovie
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string EmailEddress { get; set; }
            public string MovieName { get; set; }

            public string FullName
            {
                get 
                {
                    return $"{FirstName} {LastName}";
                }
            }

            public int Age
            {
                get 
                {
                    return DateTime.Today.Year - DateOfBirth.Year;
                }
            }
        }

        public class FavoriteMovieForm
        {
            [Required]
            [StringLength(50)]
            [DisplayName("First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50)]
            [DisplayName("Last Name")]
            public string LastName { get; set; }

            [Required]
            [DisplayName("Date Of Birth")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [StringLength(100)]
            [DisplayName("Email Address")]
            public string EmailAddress { get; set; }

            [Required]
            [StringLength(200)]
            [DisplayName("Favorite Movie")]
            public string MovieName { get; set; }
        }
    }
}
