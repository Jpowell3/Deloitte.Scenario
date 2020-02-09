using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deloitte.Scenario.BusinessLogic;
using Deloitte.Scenario.TransferModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deloitte.Scenario.Api.Controllers
{

    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IServiceCore _serviceCore;

        public CitiesController(IServiceCore serviceCore)
        {
            _serviceCore = serviceCore ?? throw new ArgumentNullException(nameof(serviceCore));
        }

        //GET api/cities/cardiff
        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult> GetCity(string name)
        {
            var response = await _serviceCore.GetCityByNameAsync(name);

            if (response == null || response.Count() == 0)
                return NotFound(name);
            else 
                return Ok(response);
        }

        //POST api/cities/cardiff
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> CreateCity([FromBody] CityAddTransferModel city)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cityId = await _serviceCore.AddCityAsync(city);

            return CreatedAtAction(nameof(CreateCity), new { id = cityId }, city);
        }

        // PUT api/cities/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCity(int id, [FromBody] CityUpdateTransferModel cityUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _serviceCore.CityExistsAsync(id))
                return NotFound(id);

            var response = await _serviceCore.UpdateCityAsync(id, cityUpdate);

            return Ok();
        }

        // DELETE api/cities/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            if (!await _serviceCore.CityExistsAsync(id))
                return NotFound(id);

            var response = await _serviceCore.DeleteCityAsync(id);

            return Ok();
        }
    }
}