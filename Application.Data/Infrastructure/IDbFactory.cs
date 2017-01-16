using System;

namespace Application.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ApplicationDbContext Init();
    }
}