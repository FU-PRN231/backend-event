using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IDbContext _db;

        public UnitOfWork(IDbContext db)
        {
            _db = db;
        }

        public async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}