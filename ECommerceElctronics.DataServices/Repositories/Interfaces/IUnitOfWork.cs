namespace ECommerceElctronics.DataServices.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IBrandRepository Brands { get; }
        IUserRepository Users { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
        Task<bool> CompleteAsync();
    }
}
