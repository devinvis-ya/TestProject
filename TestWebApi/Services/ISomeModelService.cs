using System;
using System.Threading;
using System.Threading.Tasks;

using TestWebApi.Infrastructure.Models;

namespace TestWebApi.Services
{
    public interface ISomeModelService
    {
        Task<Guid> CreateAsync(CancellationToken token);
        Task<SomeModel> FindByIdAsync(Guid id, CancellationToken token = default);
    }
}
