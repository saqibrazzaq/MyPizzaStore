using AutoMapper;
using common.Models.Exceptions;
using common.Models.Parameters;
using common.Models.Responses;
using hr.Dtos.Company;
using hr.Entities;
using hr.Repository.Contracts;
using hr.Services.Contracts;

namespace hr.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repositoryManager, 
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public CompanyDetailResponseDto Create(CreateCompanyRequestDto dto)
        {
            var companyEntity = _mapper.Map<Company>(dto);
            _repositoryManager.CompanyRepository.Create(companyEntity);
            _repositoryManager.Save();
            return _mapper.Map<CompanyDetailResponseDto>(companyEntity);
        }

        public void Delete(Guid companyId, DeleteCompanyRequestDto dto)
        {
            var companyEntity = findByCompanyIdIfExists(companyId, dto.AccountId, true);
            _repositoryManager.CompanyRepository.Delete(companyEntity);
            _repositoryManager.Save();
        }

        private Company findByCompanyIdIfExists(Guid companyId, Guid? accountId, bool trackChanges)
        {
            var companyEntity = _repositoryManager.CompanyRepository.FindByCondition(
                x => x.CompanyId == companyId && x.AccountId == accountId,
                trackChanges)
                .FirstOrDefault();
            if (companyEntity == null)
                throw new NotFoundException("No company found with id " + companyId);

            return companyEntity;
        }

        public ApiOkResponse<CompanyDetailResponseDto> FindByCompanyId(Guid companyId, FindByCompanyIdRequestDto dto)
        {
            var companyEntity = findByCompanyIdIfExists(companyId, dto.AccountId, false);
            var companyDto = _mapper.Map<CompanyDetailResponseDto>(companyEntity);
            return new ApiOkResponse<CompanyDetailResponseDto>(companyDto);
        }

        public ApiOkResponse<IEnumerable<CompanyResponseDto>> GetAll(GetAllCompaniesRequestDto dto)
        {
            var companyEntities = _repositoryManager.CompanyRepository.FindByCondition(
                x => x.AccountId == dto.AccountId,
                trackChanges: false);
            var companyDtos = _mapper.Map<IEnumerable<CompanyResponseDto>>(companyEntities);
            return new ApiOkResponse<IEnumerable<CompanyResponseDto>>(companyDtos);
        }

        public void Update(Guid companyId, UpdateCompanyRequestDto dto)
        {
            var companyEntity = findByCompanyIdIfExists(companyId, dto.AccountId, true);
            _mapper.Map(dto, companyEntity);
            _repositoryManager.Save();
        }

        public ApiOkPagedResponse<IEnumerable<CompanyResponseDto>, MetaData> Search(SearchCompaniesRequestDto dto)
        {
            return null;
        }
    }
}
