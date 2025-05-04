using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Este atributo protege todos os endpoints deste controller
    public class SensorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllSensors()
        {
            // Retorna uma lista fictícia de sensores
            var sensors = new[] { "Sensor1", "Sensor2", "Sensor3" };
            return Ok(sensors);
        }

        [HttpPost]
        public IActionResult AddSensor([FromBody] string sensorName)
        {
            // Lógica para adicionar um novo sensor (fictícia)
            return CreatedAtAction(nameof(GetAllSensors), new { name = sensorName }, sensorName);
        }
    }
}