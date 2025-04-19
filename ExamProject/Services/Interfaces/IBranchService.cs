using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.TrackDTOs;
using ExamProject.Models.Entities;

namespace ExamProject.Services.Interfaces
{
    public interface IBranchService
    {
        

        Task<IEnumerable<IndexBranchDTO>> IndexBranches();
        Task<BranchInfoDTO> GetBranch(int id);
        Task<bool> CreateBranch(CreateBranchDTO branchDto);
        Task<EditBranchDTO> EditBranch(int id);
        Task<bool> UpdateBranch(EditBranchDTO branchDto);
        Task<bool> DeleteBranch(int id);

        //Task<IEnumerable<EditBranchTracksDTO>> ServiceEditBranchTracks(int id);
        Task<bool> UpdateBranchTracks(int id, List<int> ToBeAdded, List<int> ToBeRemoved);

        //Helpers
        Task<IEnumerable<BranchTracksSelectListDTO>> AllBranchesWithTrackForSelectList(); // SelectList
        Task<BranchTracksSelectListDTO> BranchWithTrackSelectList(int id); // SelectList
    }

}
