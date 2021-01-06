using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private IMongoCollection<Shipwreck> _shipwreckCollection;
        public WeatherForecastController(IMongoClient clint)
        {
            var databse = clint.GetDatabase("sample_geospatial");
            _shipwreckCollection = databse.GetCollection<Shipwreck>("shipwrecks");
        }

        [HttpGet]
        public IEnumerable<Shipwreck> Get()
        {  
            return _shipwreckCollection.Find( s => s.FeatureType == "Wrecks - Visible").ToList() ;
        }
    }
}
