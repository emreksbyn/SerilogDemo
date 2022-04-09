using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace SerilogDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly ILogger<ModelController> _logger;

        public ModelController(ILogger<ModelController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Model model = new Model
            {
                Id = 1,
                Name = "emre",
                Date = DateTime.Now,
                IsTrue = true
            };
            _logger.LogInformation("Model başarıyla getirildi.");
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Model model1)
        {
            try
            {
                Model model2 = new Model
                {
                    Id = model1.Id,
                    Name = model1.Name,
                    Date = model1.Date,
                    IsTrue = model1.IsTrue
                };
                var json = JsonSerializer.Serialize(model2);
                _logger.LogInformation(string.Format("Model başarıyla oluşturuldu.Data:{0}",json));
                return StatusCode(201);
            }
            catch 
            {
                _logger.LogError("Model oluşturulurken bir hata meydana geldi!!");
                return BadRequest();
            }
        }
    }
}
