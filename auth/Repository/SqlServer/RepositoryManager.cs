using auth.Repository.Contracts;

namespace auth.Repository.SqlServer
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => 
                new UserRepository(context));
            _accountRepository = new Lazy<IAccountRepository>(() =>
                new AccountRepository(context));
        }

        public IUserRepository UserRepository => _userRepository.Value;

        public IAccountRepository AccountRepository => _accountRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
