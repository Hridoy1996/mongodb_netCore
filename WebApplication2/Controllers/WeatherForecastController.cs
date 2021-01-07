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
        public async Task<IEnumerable<Shipwreck>> Get()
        {
            var listWrites = new List<WriteModel<Shipwreck>>();
            var filterDefinition = Builders<Shipwreck>.Filter.Eq(p => p.FeatureType, "Wrecks - Visible");
            var updateDefinition = Builders<Shipwreck>.Update.Set(p => p.FeatureType, "Wrecks - Hridoy");
            listWrites.Add(new UpdateOneModel<Shipwreck>(filterDefinition, updateDefinition));
            await _shipwreckCollection.BulkWriteAsync(listWrites);
           // _shipwreckCollection.BulkWrite(listWrites);
            var tmp =  _shipwreckCollection.Find(s => s.FeatureType == "Wrecks - Hridoy").ToList();
            return tmp;
        }
    }
}
