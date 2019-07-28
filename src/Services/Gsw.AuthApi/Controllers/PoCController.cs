using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GSW.AuthApi.Controllers
{
    [Route("api/[controller]")]
    public class PoCController : Controller
    {
        public List<Weather> Index()
        {
           return new List<Weather>()
           {
               new Weather{Summary = "Test1", TempC = 1},
               new Weather{Summary = "Test2", TempC = 2},
             new Weather{Summary = "Test3", TempC = 3},
           };
        }


    }

    public class Weather
    {
        public int TempC { get; set; }
        public string Summary { get; set; }

    }
}