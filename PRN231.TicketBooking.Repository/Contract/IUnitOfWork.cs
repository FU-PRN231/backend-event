namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IUnitOfWork
    {
        public Task SaveChangeAsync();

        void Dispose();
    }
}