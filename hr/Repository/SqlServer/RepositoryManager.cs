using hr.Repository.Contracts;

namespace hr.Repository.SqlServer
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IBranchRepository> _branchRepository;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IDesignationRepository> _designationRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IGenderRepository> _genderRepository;
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _branchRepository = new Lazy<IBranchRepository>(() =>
                new BranchRepository(_context));
            _companyRepository = new Lazy<ICompanyRepository>(() =>
                new CompanyRepository(_context));
            _departmentRepository = new Lazy<IDepartmentRepository>(() =>
                new DepartmentRepository(_context));
            _designationRepository = new Lazy<IDesignationRepository>(() =>
                new DesignationRepository(_context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() =>
                new EmployeeRepository(_context));
            _genderRepository = new Lazy<IGenderRepository>(() =>
                new GenderRepository(_context));
        }
        public IBranchRepository BranchRepository => _branchRepository.Value;

        public ICompanyRepository CompanyRepository => _companyRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public IDesignationRepository DesignationRepository => _designationRepository.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IGenderRepository GenderRepository => _genderRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
