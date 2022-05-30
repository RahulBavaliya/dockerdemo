using dockerdemocrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace dockerdemocrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly String connectionString = "Data Source=10.0.62.81,1434;Initial Catalog=crudedemo;User ID=sa; Password=Pass@123;Type System Version= Latest;Connection Timeout = 30;Application Name = IITGN Portal";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        List<Movie> movies = new List<Movie>();
        public IActionResult Index()
        {
            //context.Movie.ToList();
            var m = "a";//we need to return list of movie 

            for(int i = 0; i < 25; i++)
            {
                Movie movie = new Movie();
                movie.Id = i.ToString()+1;
                movie.Name = "ABCD";
                movie.Actors = "Don't know";
                movies.Add(movie);
            }
            



            var m1 = movies.ToList();

            return View(m1);
        }

        [HttpGet]
        [Route("Home/getMovieList")]
        public List<Movie> getMovieList()
        {
            DataSet dsActionandLeaveData = new DataSet();
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand command1 = new SqlCommand("crudedemo..getmovie", conn);
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);

            dataAdapter1.Fill(dsActionandLeaveData);

            conn.Close();

            DataTable dt = dsActionandLeaveData.Tables[0];

            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movie movie = new Movie();

                movie.Id = dt.Rows[i]["Id"].ToString();
                movie.Name = dt.Rows[i]["Name"].ToString();
                movie.Actors = dt.Rows[i]["Actors"].ToString();

                movies.Add(movie);
            }
            return movies;
        }


        [HttpPost]
        [Route("Home/sendData")]
        public List<Movie> sendData([FromBody()] Movie movie1)
        {

            DataSet dsActionandLeaveData = new DataSet();
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand command1 = new SqlCommand("crudedemo..addmovie", conn);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.Add(new SqlParameter("@Id", movie1.Id));
            command1.Parameters.Add(new SqlParameter("@Name", movie1.Name));
            command1.Parameters.Add(new SqlParameter("@Actors", movie1.Actors));
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);

            dataAdapter1.Fill(dsActionandLeaveData);

            conn.Close();

            DataTable dt = dsActionandLeaveData.Tables[0];

            var m = movie1;//we need to return list of movie 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movie movie = new Movie();

                movie.Id = dt.Rows[i]["Id"].ToString();
                movie.Name = dt.Rows[i]["Name"].ToString();
                movie.Actors = dt.Rows[i]["Actors"].ToString();

                movies.Add(movie);
            }

            return movies;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


       

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie m)
        {
            if (ModelState.IsValid)
            {
                // we need to pass as a parameter as movie object m
                //Add new data into db using sp or qeury
                movies.Add(m);
                return RedirectToAction("Index");
            }
            else
                return View();
        }

       
        public IActionResult Update(int id)
        {
            //var pc = context.Movie.Where(a => a.Id == id).FirstOrDefault();
            var pc = "sdf";//we need to update movie object data in db 
            return View(pc);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Movie m)
        {
            if (ModelState.IsValid)
            {
                
                var pc = "sdf";//we need to update movie object data in db 

                return RedirectToAction("Index");
            }
            else
                return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //var pc = context.Movie.Where(a => a.Id == id).FirstOrDefault();
            //context.Remove(pc);
            //await context.SaveChangesAsync();

            //we need to delete data from db where id 

            return RedirectToAction("Index");
        }
    }
}
