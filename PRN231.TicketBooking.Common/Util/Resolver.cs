namespace PRN231.TicketBooking.Common.Util
{
    public abstract class Resolver
    {
        private readonly IServiceProvider _serviceProvider;

        public Resolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T? Resolve<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T))!;
        }
    }
}