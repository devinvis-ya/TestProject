using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TestWebApi.Converters;
using TestWebApi.Infrastructure.Models.Enums;
using TestWebApi.Infrastructure.Repositories;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("/task")]
    public class SomeModelController : ControllerBase
    {
        private readonly ISomeModelRepository _someModelRepository;

        public SomeModelController(ISomeModelRepository someModelRepository)
        {
            _someModelRepository = someModelRepository ?? throw new ArgumentNullException(nameof(someModelRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel()
        {
            //Зашитая передача токена отмены
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            var createdModel = await _someModelRepository.CreateAsync(token: tokenSource.Token);
            await _someModelRepository.SaveChangesAsync(tokenSource.Token);

            _ = Task.Run(async () =>
            {
                createdModel.Status = StateStatus.Running;
                await _someModelRepository.UpdateAsync(createdModel, tokenSource.Token);
                await _someModelRepository.SaveChangesAsync(tokenSource.Token);

                await Task.Delay(2 * 60 * 1000);

                createdModel.Status = StateStatus.Finished;
                await _someModelRepository.UpdateAsync(createdModel, tokenSource.Token);
                await _someModelRepository.SaveChangesAsync(tokenSource.Token);

            });

            return Accepted(new Uri($"http://localhost:5000/task/{createdModel.Id}"), createdModel.Id);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel([Required] Guid id)
        {
            var model = await _someModelRepository.FindByIdAsync(id);

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
