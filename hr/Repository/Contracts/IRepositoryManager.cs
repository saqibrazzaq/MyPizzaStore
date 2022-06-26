namespace hr.Repository.Contracts
{
    public interface IRepositoryManager
    {
        IBranchRepository BranchRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IGenderRepository GenderRepository { get; }
        void Save();
    }
}
