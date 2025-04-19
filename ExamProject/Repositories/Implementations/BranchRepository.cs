using ExamProject.Models.Data;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories.Implementations
{
    public class BranchRepository(AppDbContext context) : BaseRepository<Branch>(context), IBranchRepository
    {

        //BranchService

        //Uses
        // GetAllBranchesAsync (BranchService)
        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()  
            => await _context.Branches
                .ToListAsync();

        //Uses
        // GetBranchWithTracksAndStudentsAsync (BranchService)
        public async Task<Branch> GetBranchWithTracksAndStudentsAsync(int branchId) 
            => await _context.Branches
                .Include(b => b.BranchTracks)
                    .ThenInclude(bt => bt.Track)
                .Include(b => b.BranchTracks)
                    .ThenInclude(bt => bt.Students)
                .FirstOrDefaultAsync(b => b.Id == branchId);
        
        //UnUsed
        public async Task<bool> BranchExistsAsync(int branchId) 
            => await _context.Branches
                .AnyAsync(b => b.Id == branchId);

        //Helpers

        //Uses

        public async Task<IEnumerable<Branch>> GetAllBranchesWithTracksAsync()   // SelectList
            => await _context.Branches
                .Include(b => b.BranchTracks) 
                    .ThenInclude(bt => bt.Track)
                .ToListAsync();


        public async Task<Branch> GetBranchWithTracksAsync(int branchId)   // UnUsed
            => await _context.Branches
                .Include(b => b.BranchTracks) 
                    .ThenInclude(bt => bt.Track)
                .FirstOrDefaultAsync(b => b.Id == branchId);
    }

}
