namespace auth.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IAccountRepository AccountRepository { get; }
        Task SaveAsync();
    }
}
