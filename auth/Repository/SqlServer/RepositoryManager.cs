using auth.Repository.Contracts;

namespace auth.Repository.SqlServer
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _userRepository = new Lazy<IUserRepository>(() => 
                new UserRepository(context));
        }

        private readonly Lazy<IUserRepository> _userRepository;
        public IUserRepository UserRepository => _userRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
