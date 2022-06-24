namespace cities.Repository.Contracts
{
    public interface IRepositoryManager
    {
        ICountryRepository CountryRepository { get; }
        IStateRepository StateRepository { get; }
        ITimeZoneRepository TimeZoneRepository { get; }
        ICityRepository CityRepository { get; }
        void Save();
    }
}
