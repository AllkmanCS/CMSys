using System.Reflection;
using CMSys.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure;

internal class AppContext : DbContext
{
    internal AppContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
    }
}