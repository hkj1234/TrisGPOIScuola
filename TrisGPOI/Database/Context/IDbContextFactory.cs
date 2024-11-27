namespace TrisGPOI.Database.Context
{
    public interface IDbContextFactory
    {
        public ApplicationDbContext CreateMySQLDbContext();
    }
}
