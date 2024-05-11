namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IUnitOfWork
    {
        Task SaveChangeAsync();
        void Dispose();

    }
}