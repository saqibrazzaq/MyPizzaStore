namespace auth.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
