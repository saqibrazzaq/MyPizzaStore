using cities.Repository.Contracts;

namespace cities.Repository.SqlServer
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<ITimeZoneRepository> _timeZoneRepository;
        private readonly Lazy<IStateRepository> _stateRepository;
        private readonly Lazy<ICityRepository> _cityRepository;

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _countryRepository = new Lazy<ICountryRepository>(() =>
                new CountryRepository(context));
            _timeZoneRepository = new Lazy<ITimeZoneRepository>(() =>
                new TimeZoneRepository(context));
            _cityRepository = new Lazy<ICityRepository>(() =>
                new CityRepository(context));
            _stateRepository = new Lazy<IStateRepository>(() =>
                new StateRepository(context));
        }

        public ICountryRepository CountryRepository => _countryRepository.Value;

        public IStateRepository StateRepository => _stateRepository.Value;

        public ITimeZoneRepository TimeZoneRepository => _timeZoneRepository.Value;

        public ICityRepository CityRepository => _cityRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
