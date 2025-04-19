using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.TrackDTOs;

namespace ExamProject.Services.Interfaces
{
    public interface ITrackService
    {
        Task<IEnumerable<IndexTrackDTO>> IndexTrackes();
        Task<GetTrackDTO> GetTrack(int id);
        Task<bool> CreateTrack(CreateTrackDTO trackDto);
        Task<EditTrackDTO> EditTrack(int id);
        Task<bool> UpdateTrack(EditTrackDTO trackDto);
        Task<bool> DeleteTrack(int id);
        Task<IEnumerable<TrackSelectListDTO>> TracksSelectList();
    }
}
