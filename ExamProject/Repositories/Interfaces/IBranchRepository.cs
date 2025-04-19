using ExamProject.Models.Entities;

namespace ExamProject.Repositories.Interfaces
{
    public interface IBranchRepository : IRepository<Branch>
    {

        Task<IEnumerable<Branch>> GetAllBranchesAsync();  // GetAllBranchesAsync (BranchService)
        Task<IEnumerable<Branch>> GetAllBranchesWithTracksAsync(); // GetBranchWithTracksAndStudentsAsync (BranchService)
        Task<Branch> GetBranchWithTracksAndStudentsAsync(int branchId);
        Task<Branch> GetBranchWithTracksAsync(int branchId);
        Task<bool> BranchExistsAsync(int branchId);
    }
}
