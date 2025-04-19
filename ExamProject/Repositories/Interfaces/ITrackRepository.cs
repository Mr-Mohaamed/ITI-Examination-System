using ExamProject.Models.Entities;

namespace ExamProject.Repositories.Interfaces
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<IEnumerable<Track>> GetAllTracksWithBranchesAsync();
        Task<Track> GetTrackWithBranchesAndStudentsAsync(int trackId);
        Task<bool> TrackExistsAsync(int trackId);
    }
}
