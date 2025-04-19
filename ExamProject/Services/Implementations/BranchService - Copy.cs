//using ExamProject.Models.DTOs.BranchDTOs;
//using ExamProject.Models.Entities;
//using ExamProject.Repositories.Interfaces;
//using ExamProject.Services.Interfaces;

//namespace ExamProject.Services.Implementations
//{
//    public class BranchService(IBranchRepository branchRepository, ITrackRepository trackRepository) : IBranchService
//    {
//        private readonly IBranchRepository _branchRepository = branchRepository;
//        private readonly ITrackRepository _trackRepository = trackRepository;

//        public async Task<IEnumerable<IndexBranchDTO>> ServiceGetAllBranchesAsync()
//        {
//            var branches = await _branchRepository.GetAllBranchesAsync();
//            return branches.Select(b => new IndexBranchDTO
//            {
//                Id = b.Id,
//                Name = b.Name,
//            });
//        }

//        public async Task<BranchInfoDTO> ServiceGetByIdAsync(int id)
//        {
//            var branches = await _branchRepository.GetBranchWithTracksAndStudentsAsync(id);
//            if (branches == null)
//                return null;
//            var branchesDtos = new BranchInfoDTO
//            {
//                Id = branches.Id,
//                Name = branches.Name,
//                Tracks = branches.BranchTracks.Select(bt => new TrackInfoDTO
//                {
//                    Id = bt.Track.Id,
//                    Name = bt.Track.Name,
//                    StudentsCount = bt.Students.Count(),
//                    Students = bt.Students.Select(s => new StudentInfoDTO
//                    {
//                        Id = s.Id,
//                        Name = s.Name
//                    }).ToList()
//                }).ToList()
//            };
//            return branchesDtos;
//        }

//        public async Task<int> ServiceAddAsync(CreateBranchDTO branchDto)
//        {
//            Branch branch = new Branch
//            {
//                Name = branchDto.Name,
//            };
//            await _branchRepository.AddAsync(branch);
//            await _branchRepository.SaveAsync();
//            return branch.Id;
//        }

//        public async Task<EditBranchDTO> ServiceEditAsync(int id)
//        {
//            var branch = await _branchRepository.GetByIdAsync(id);
//            if (branch == null)
//                return null;
//            var branchDto = new EditBranchDTO
//            {
//                Id = branch.Id,
//                Name = branch.Name,
//            };
//            return branchDto;
//        }
//        public async Task ServiceUpdateAsync(EditBranchDTO branchDto)
//        {
//            var branch = await _branchRepository.GetByIdAsync(branchDto.Id);
//            if (branch == null)
//                return;
//            branch.Name = branchDto.Name;
//            await _branchRepository.UpdateAsync(branch);
//            await _branchRepository.SaveAsync();
//        }

//        public async Task ServiceDeleteAsync(int id)
//        {
//            var branch = await _branchRepository.GetByIdAsync(id);
//            if (branch != null)
//            {
//                await _branchRepository.DeleteAsync(branch);
//                await _branchRepository.SaveAsync();
//            }
//        }

//        public async Task ServiceUpdateBranchTracks(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
//        {

//            if (ToBeAdded == null && ToBeRemoved == null)
//                return;
//            var branch = await _branchRepository.GetByIdAsync(id);
//            if (branch == null)
//                return;
//            // Add new tracks
//            foreach (var trackId in ToBeAdded)
//            {
//                var track = await _trackRepository.GetByIdAsync(trackId);
//                if (track != null)
//                {
//                    branch.BranchTracks.Add(new BranchTrack { BranchId = branch.Id, TrackId = trackId });
//                }
//            }
//            // Remove tracks
//            foreach (var trackId in ToBeRemoved)
//            {
//                var track = await _trackRepository.GetByIdAsync(trackId);
//                if (track != null)
//                {
//                    branch.BranchTracks.Remove(new BranchTrack { BranchId = branch.Id, TrackId = trackId });
//                }
//            }
//            await _branchRepository.UpdateAsync(branch);
//            await _branchRepository.SaveAsync();
//        }


//        //Get : Branch/{id}/Tracks
//        //public async Task<EditBranchTracksDTO> ServiceEditBranchTracks(int id)
//        //{
//        //    var branch = await _branchRepository.GetBranchWithTracksAsync(id);
//        //    if (branch == null)
//        //        return null;
//        //    var branchDto = new EditBranchTracksDTO
//        //    {
//        //        Id = branch.Id,
//        //        Name = branch.Name,
//        //        Tracks = branch.BranchTracks.Select(bt => new EditBranchTrackInfoDTO
//        //        {
//        //            Id = bt.Track.Id,
//        //            Name = bt.Track.Name,
//        //        }).ToList()
//        //    };
//        //    return branchDto;
//        //    //return Task.FromResult(branchTracks);
//        //}

//        // Helpers

//        //Uses
//        // Create : (StudentController)
//        public async Task<IEnumerable<BranchWithTracksSelectListDTO>> AllBranchesWithTrackForSelectList()
//        {
//            var branches = await _branchRepository.GetAllBranchesWithTracksAsync();
//            return branches.Select(b => new BranchWithTracksSelectListDTO
//            {
//                BranchId = b.Id,
//                BranchName = b.Name,
//                Tracks = b.BranchTracks.Select(bt => new TrackSelectListDTO
//                {
//                    TrackId = bt.Track.Id,
//                    TrackName = bt.Track.Name,
//                }).ToList()
//            });
//        }

//        public async Task<BranchWithTracksSelectListDTO> BranchWithTrackForSelectList(int id)
//        {
//            var branch = await _branchRepository.GetBranchWithTracksAsync(id);
//            if (branch == null)
//                return null;
//            var branchDto = new BranchWithTracksSelectListDTO
//            {
//                BranchId = branch.Id,
//                BranchName = branch.Name,
//                Tracks = branch.BranchTracks.Select(bt => new TrackSelectListDTO
//                {
//                    TrackId = bt.Track.Id,
//                    TrackName = bt.Track.Name,
//                }).ToList()
//            };
//            return branchDto;
//        }

//    }

//}
