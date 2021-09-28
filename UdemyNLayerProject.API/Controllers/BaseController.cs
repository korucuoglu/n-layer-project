using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.Service;

namespace UdemyNLayerProject.API.Controllers
{

   
    public class BaseController : Controller
    {
        protected readonly RedisService _redisService;
        public BaseController(RedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
