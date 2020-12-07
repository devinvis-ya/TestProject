using System;
using System.Threading;
using System.Threading.Tasks;

using TestWebApi.Infrastructure.Models;
using TestWebApi.Infrastructure.Models.Enums;
using TestWebApi.Infrastructure.Repositories;

namespace TestWebApi.Services
{
    public class SomeModelService : ISomeModelService
    {
        private readonly ISomeModelRepository _someModelRepository;

        public SomeModelService(ISomeModelRepository someModelRepository)
        {
            _someModelRepository = someModelRepository ?? throw new ArgumentNullException(nameof(someModelRepository));
        }

        public async Task<Guid> CreateAsync(CancellationToken token)
        {
            var createdModel = await _someModelRepository.CreateAsync(token: token);
            await _someModelRepository.SaveChangesAsync(token);

            _ = Task.Run(async () =>
            {
                createdModel.Status = StateStatus.Running;
                await _someModelRepository.UpdateAsync(createdModel, token);
                await _someModelRepository.SaveChangesAsync(token);

                //await Task.Delay(2 * 60 * 1000);
                await Task.Delay(10000);

                createdModel.Status = StateStatus.Finished;
                await _someModelRepository.UpdateAsync(createdModel, token);
                await _someModelRepository.SaveChangesAsync(token);

            });

            return createdModel.Id;
        }

        public async Task<SomeModel> FindByIdAsync(Guid id, CancellationToken token)
            => await _someModelRepository.FindByIdAsync(id, token);
    }
}
