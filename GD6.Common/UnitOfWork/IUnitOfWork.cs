using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GD6.Common
{
    public interface IUnitOfWork
    {
        DbContext Current { get; }

        void Commit();
        void Rollback();
        IDbContextTransaction StartTransaction();
    }
}
