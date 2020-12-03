using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TestWebApi.Infrastructure.Models;
using TestWebApi.Infrastructure.Models.Enums;

namespace TestWebApi.Infrastructure.Repositories
{
    public class SomeModelRepository : ISomeModelRepository
    {
        private readonly DefaultDbContext _dbContext;
        public SomeModelRepository(DefaultDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<SomeModel> CreateAsync(CancellationToken token)
            => (await _dbContext.SomeModels.AddAsync(new SomeModel(StateStatus.Created))).Entity;

        public async Task<SomeModel> FindByIdAsync(Guid id, CancellationToken token = default)
            => await _dbContext.SomeModels.FirstOrDefaultAsync(x => x.Id == id, token);

        public async Task UpdateAsync(SomeModel model, CancellationToken token)
        {
            var existingSomeModel = await _dbContext.SomeModels.FindAsync(new object[] { model.Id }, token);

            if (existingSomeModel != null)
            {
                model.TimeStamp = DateTime.Now;
                _dbContext.Entry(existingSomeModel).CurrentValues.SetValues(model);
            }
        }

        public async Task SaveChangesAsync(CancellationToken token) => await _dbContext.SaveChangesAsync(token);
    }
}
