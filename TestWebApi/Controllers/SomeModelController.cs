using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TestWebApi.Converters;
using TestWebApi.Services;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("/task")]
    public class SomeModelController : ControllerBase
    {
        private readonly ISomeModelService _someModelService;

        public SomeModelController(ISomeModelService someModelService)
        {
            _someModelService = someModelService ?? throw new ArgumentNullException(nameof(someModelService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel()
        {
            //Зашитая передача токена отмены
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            var createdModelId = await _someModelService.CreateAsync(token: tokenSource.Token);

            return Accepted(new Uri($"http://localhost:5000/task/{createdModelId}"), createdModelId);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel([Required] Guid id)
        {
            var model = await _someModelService.FindByIdAsync(id);

            return model switch
            {
                null => NotFound(),
                _ => Ok(new
                {
                    status = model.Status.GetKeyName(),
                    timestamp = model.TimeStamp.ToStringISO8601()
                })
            };
        }
    }
}
