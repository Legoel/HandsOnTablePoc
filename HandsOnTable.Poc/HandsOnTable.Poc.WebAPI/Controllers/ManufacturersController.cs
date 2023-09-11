using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using HandsOnTable.Poc.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace HandsOnTable.Poc.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ILogger<ManufacturersController> _logger;

        public ManufacturersController(IManufacturerRepository manufacturerRepository, ILogger<ManufacturersController> logger)
        {
            _manufacturerRepository = manufacturerRepository;
            _logger = logger;
        }

        [HttpPost(Name = "PostManufacturer")]
        public bool Create([FromBody] Manufacturer manufacturer) => _manufacturerRepository.Create(manufacturer);

        [HttpGet("{id}", Name = "GetManufacturerById")]
        public Manufacturer? GetById([FromRoute] int id) => _manufacturerRepository.GetById(id);

        [HttpGet("name/{name}", Name = "GetManufacturerByName")]
        public Manufacturer? GetByName([FromRoute] string name) => _manufacturerRepository.GetByName(name);

        [HttpGet(Name = "GetManufacturerList")]
        public ICollection<Manufacturer> GetAll() => _manufacturerRepository.GetAll();

        [HttpPut("{id}", Name = "PutManufacturer")]
        public Manufacturer Upsert([FromRoute] int id, [DisallowNull, FromBody] Manufacturer manufacturer) => _manufacturerRepository.Upsert(id, manufacturer);

        [HttpDelete("{id}", Name = "DeleteManufacturer")]
        public bool Delete(int id) => _manufacturerRepository.Delete(id);
    }
}
