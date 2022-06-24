using cities.Services.Contracts;

namespace cities.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly IResetCityStateCountriesService _resetCityStateCountriesService;

        public DataSeedService(IResetCityStateCountriesService resetCityStateCountriesService)
        {
            _resetCityStateCountriesService = resetCityStateCountriesService;
        }

        public void ResetCityStateCountries()
        {
            _resetCityStateCountriesService.ResetAllData();
        }
    }
}
