using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.TrackDTOs;
using ExamProject.Models.Entities;
using ExamProject.Repositories.Interfaces;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class TrackService(IRepository<Track> trackRepository) : ITrackService
    {
        private readonly IRepository<Track> _trackRepository = trackRepository;

        public async Task<IEnumerable<IndexTrackDTO>> IndexTrackes()
        {
            var tracks = await _trackRepository.GetAllAsync(t => t.BranchTracks);
            return tracks.Select(t => new IndexTrackDTO
            {
                Id = t.Id,
                Name = t.Name,
                Duration = t.Duration,
                BranchesCount = t.BranchTracks.Count()
            });
        }

        public async Task<GetTrackDTO> GetTrack(int id)
        {
            var track = await _trackRepository.GetByIdWithNestedIncludesAsync(id, ["BranchTracks.Branch", "BranchTracks.Students"]);
            if (track == null)
                return null;
            var trackDto = new GetTrackDTO
            {
                Id = track.Id,
                Name = track.Name,
                Description = track.Description,
                Duration = track.Duration,
                Branches = track.BranchTracks.Select(bt => new GetTrackInfoDTO
                {
                    BranchId = bt.Branch.Id,
                    BranchName = bt.Branch.Name,
                    numberOfStudents = bt.Students.Count(),
                    Students = bt.Students.Select(s => new GetTrackInfoStudentDTO
                    {
                        StudentId = s.Id,
                        StudentName = s.Name
                    }).ToList()
                }).ToList()
            };
            return trackDto;
        }

        public async Task<bool> CreateTrack(CreateTrackDTO trackDto)
        {
            Track track = new Track
            {
                Name = trackDto.Name,
                Description = trackDto.Description,
                Duration = trackDto.Duration
            };
            await _trackRepository.AddAsync(track);
            return true;

        }
        public async Task<EditTrackDTO> EditTrack(int id)
        {
            var track = await _trackRepository.GetByIdAsync(id);
            if (track == null)
                return null;
            var trackDto = new EditTrackDTO
            {
                Id = track.Id,
                Name = track.Name,
                Description = track.Description,
                Duration = track.Duration
            };
            return trackDto;
        }

        public async Task<bool> UpdateTrack(EditTrackDTO trackDto)
        {
            var track = await _trackRepository.GetByIdAsync(trackDto.Id);
            if (track == null)
                return false;

            track.Name = trackDto.Name;
            track.Description = trackDto.Description;
            track.Duration = trackDto.Duration;

            try
            {
                await _trackRepository.UpdateAsync(track);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteTrack(int id)
        {
            var track = await _trackRepository.GetByIdAsync(id);
            if (track != null)
            {
                await _trackRepository.DeleteAsync(id);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TrackSelectListDTO>> TracksSelectList()
        {
            var tracks = await _trackRepository.GetAllAsync();
            return tracks.Select(t => new TrackSelectListDTO
            {
                TrackId = t.Id,
                TrackName = t.Name
            }).ToList();
        }

    }
}
