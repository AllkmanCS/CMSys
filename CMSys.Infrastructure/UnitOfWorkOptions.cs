namespace CMSys.Infrastructure;

public sealed class UnitOfWorkOptions
{
    public string ConnectionString { get; set; }
    public int? CommandTimeout { get; set; }
}