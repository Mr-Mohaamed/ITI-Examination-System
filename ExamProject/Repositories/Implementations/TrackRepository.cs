using ExamProject.Models.Data;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Repositories.Implementations
{
    public class TrackRepository(AppDbContext context) : BaseRepository<Track>(context), ITrackRepository
    {
        public async Task<IEnumerable<Track>> GetAllTracksWithBranchesAsync()
            => await _context.Tracks
                .Include(t => t.BranchTracks)
                    //.ThenInclude(bt => bt.Branch)
                .ToListAsync();
        public async Task<Track> GetTrackWithBranchesAndStudentsAsync(int trackId)
            => await _context.Tracks 
                .Include(t => t.BranchTracks)
                    .ThenInclude(bt => bt.Branch)
                .Include(t => t.BranchTracks)
                    .ThenInclude(bt => bt.Students)
                .FirstOrDefaultAsync(t => t.Id == trackId);
        public async Task<bool> TrackExistsAsync(int trackId)
            => await _context.Tracks
                .AnyAsync(t => t.Id == trackId);
    }
}
