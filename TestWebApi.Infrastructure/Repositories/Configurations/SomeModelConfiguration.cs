
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TestWebApi.Infrastructure.Models;

namespace TestWebApi.Infrastructure.Repositories.Configurations
{
    public class SomeModelConfiguration : IEntityTypeConfiguration<SomeModel>
    {
        public void Configure(EntityTypeBuilder<SomeModel> builder)
        {
            builder.ToTable("some_models").HasKey(column => column.Id);
        }
    }
}
