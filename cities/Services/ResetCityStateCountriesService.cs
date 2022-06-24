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

        public void ResetAllData()
        {
            DeleteAllData();
            ImportAllData();
            
        }

        private void ImportAllData()
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
                _repository.Save();
                Console.WriteLine(countries.Length + " countries imported.");
            }
        }

        private void DeleteAllData()
        {
            foreach(var tzEntity in _repository.TimeZoneRepository.FindAll(trackChanges: true))
            {
                _repository.TimeZoneRepository.Delete(tzEntity);
            }
            _repository.Save();

            foreach (var cityEntity in _repository.CityRepository.FindAll(trackChanges: true))
            {
                _repository.CityRepository.Delete(cityEntity);
            }
            _repository.Save();

            foreach(var stateEntity in _repository.StateRepository.FindAll(trackChanges: true))
            {
                _repository.StateRepository.Delete(stateEntity);
            }
            _repository.Save();

            foreach (var countryEntity in _repository.CountryRepository.FindAll(trackChanges: true))
            {
                _repository.CountryRepository.Delete(countryEntity);
            }
            _repository.Save();
        }
    }
}
