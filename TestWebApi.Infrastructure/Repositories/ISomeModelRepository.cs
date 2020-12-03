using System;
using System.Threading;
using System.Threading.Tasks;

using TestWebApi.Infrastructure.Models;

namespace TestWebApi.Infrastructure.Repositories
{
    public interface ISomeModelRepository
    {
        Task<SomeModel> CreateAsync(CancellationToken token);
        Task<SomeModel> FindByIdAsync(Guid id, CancellationToken token = default);
        Task UpdateAsync(SomeModel model, CancellationToken token);
        Task SaveChangesAsync(CancellationToken token);
    }
}
