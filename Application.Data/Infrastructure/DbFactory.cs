
namespace Application.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ApplicationDbContext dbContext;

        public ApplicationDbContext Init()
        {
            return dbContext ?? (dbContext = new ApplicationDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}