using AutoMapper;
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

        public void Create(CreateCompanyRequestDto dto)
        {
            var companyEntity = _mapper.Map<Company>(dto);
            _repositoryManager.CompanyRepository.Create(companyEntity);
            _repositoryManager.Save();
        }

        public void Delete(Guid companyId, DeleteCompanyRequestDto dto)
        {
            
        }

        public ApiOkResponse<CompanyDetailResponseDto> FindByCompanyId(Guid companyId, FindByCompanyIdRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public ApiOkResponse<IEnumerable<CompanyResponseDto>> GetAll(GetAllCompaniesRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid companyId, UpdateCompanyRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
