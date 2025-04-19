using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.TopicDTOs;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class BranchService(IRepository<Branch> branchRepository, IRepository<Track> trackRepository) : IBranchService
    {
        private readonly IRepository<Branch> _branchRepository = branchRepository;
        private readonly IRepository<Track> _trackRepository = trackRepository;

        public async Task<IEnumerable<IndexBranchDTO>> IndexBranches()
        {
            var branches = await _branchRepository.GetAllAsync();
            return branches.Select(b => new IndexBranchDTO
            {
                Id = b.Id,
                Name = b.Name,
            });
        }

        public async Task<BranchInfoDTO> GetBranch(int id)
        {
            var branch = await _branchRepository.GetByIdWithNestedIncludesAsync(id, ["BranchTracks.Track", "BranchTracks.Students"]);
            if (branch == null)
                return null;
            var branchDtos = new BranchInfoDTO
            {
                Id = branch.Id,
                Name = branch.Name,
                Tracks = branch.BranchTracks.Select(bt => new TrackInfoDTO
                {
                    Id = bt.Track.Id,
                    Name = bt.Track.Name,
                    StudentsCount = bt.Students.Count(),
                    Students = bt.Students.Select(s => new StudentInfoDTO
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                }).ToList()
            };
            return branchDtos;
        }

        public async Task<bool> CreateBranch(CreateBranchDTO branchDto)
        {
            Branch branch = new Branch
            {
                Name = branchDto.Name,
            };
            await _branchRepository.AddAsync(branch);
            return true;
        }

        public async Task<EditBranchDTO> EditBranch(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
                return null;
            var branchDto = new EditBranchDTO
            {
                Id = branch.Id,
                Name = branch.Name,
            };
            return branchDto;
        }
        public async Task<bool> UpdateBranch(EditBranchDTO branchDto)
        {
            var branch = await _branchRepository.GetByIdAsync(branchDto.Id);
            if (branch == null)
                return false;
            branch.Name = branchDto.Name;
            await _branchRepository.UpdateAsync(branch);
            return true;
        }

        public async Task<bool> DeleteBranch(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return false;
            }
            await _branchRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> UpdateBranchTracks(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
        {

            if (ToBeAdded == null && ToBeRemoved == null)
                return false;
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
                return false;
            // Add new tracks
            foreach (var trackId in ToBeAdded)
            {
                var track = await _trackRepository.GetByIdAsync(trackId);
                if (track != null)
                {
                    branch.BranchTracks.Add(new BranchTrack { BranchId = branch.Id, TrackId = trackId });
                }
            }
            // Remove tracks
            foreach (var trackId in ToBeRemoved)
            {
                var track = await _trackRepository.GetByIdAsync(trackId);
                if (track != null)
                {
                    branch.BranchTracks.Remove(new BranchTrack { BranchId = branch.Id, TrackId = trackId });
                }
            }
            await _branchRepository.UpdateAsync(branch);
            return true;
        }

        public async Task<IEnumerable<BranchTracksSelectListDTO>> AllBranchesWithTrackForSelectList()
        {
            var branches = await _branchRepository.GetAllWithNestedIncludesAsync("BranchTracks.Track");
            return branches.Select(b => new BranchTracksSelectListDTO
            {
                BranchId = b.Id,
                BranchName = b.Name,
                Tracks = b.BranchTracks.Select(bt => new TrackSelectListDTO
                {
                    TrackId = bt.Track.Id,
                    TrackName = bt.Track.Name,
                }).ToList()
            });
        }

        public async Task<BranchTracksSelectListDTO> BranchWithTrackSelectList(int id)
        {
            var branch = await _branchRepository.GetByIdWithNestedIncludesAsync(id, "BranchTracks.Track");
            if (branch == null)
                return null;
            var branchDto = new BranchTracksSelectListDTO
            {
                BranchId = branch.Id,
                BranchName = branch.Name,
                Tracks = branch.BranchTracks.Select(bt => new TrackSelectListDTO
                {
                    TrackId = bt.Track.Id,
                    TrackName = bt.Track.Name,
                }).ToList()
            };
            return branchDto;
        }

    }

}
