using AutoMapper;
using cities.Repository.Contracts;
using cities.Services.Contracts;
using System.Text.Json;

namespace cities.Services
{
    public class ResetCityStateCountriesService : IResetCityStateCountriesService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ResetCityStateCountriesService(IMapper mapper, 
            IRepositoryManager repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task ResetAllData()
        {
            await DeleteAllData();
            await ImportAllData();
            
        }

        private async Task ImportAllData()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "Data", "countries+states+cities.json");
            if (File.Exists(file))
            {
                Console.WriteLine("Found json file");

                string jsonString = File.ReadAllText(file);
                var countries = JsonSerializer.Deserialize<Models.DataSeed.Cities.Country[]>(jsonString);
                var countriesEntity = _mapper.Map<Entities.Country[]>(countries);
                foreach (var countryEntity in countriesEntity)
                {
                    _repository.CountryRepository.Create(countryEntity);
                }
                await _repository.SaveAsync();
                Console.WriteLine(countries.Length + " countries imported.");
            }
        }

        private async Task DeleteAllData()
        {
            foreach(var tzEntity in _repository.TimeZoneRepository.FindAll(trackChanges: true))
            {
                _repository.TimeZoneRepository.Delete(tzEntity);
            }
            await _repository.SaveAsync();

            foreach (var cityEntity in _repository.CityRepository.FindAll(trackChanges: true))
            {
                _repository.CityRepository.Delete(cityEntity);
            }
            await _repository.SaveAsync();

            foreach(var stateEntity in _repository.StateRepository.FindAll(trackChanges: true))
            {
                _repository.StateRepository.Delete(stateEntity);
            }
            await _repository.SaveAsync();

            foreach(var countryEntity in _repository.CountryRepository.FindAll(trackChanges: true))
            {
                _repository.CountryRepository.Delete(countryEntity);
            }
            await _repository.SaveAsync();
        }
    }
}
