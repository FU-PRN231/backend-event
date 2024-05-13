using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContext _db;

        public UnitOfWork(IDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}