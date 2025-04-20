using ExamProject.Models.DTOs.BranchDTOs;
using ExamProject.Models.DTOs.TrackDTOs;
using ExamProject.Models.Entities;
using ExamProject.Services.Interfaces;

namespace ExamProject.Services.Implementations
{
    public class TrackService(IRepository<Track> trackRepository, IRepository<Course> courseRepository) : ITrackService
    {
        private readonly IRepository<Track> _trackRepository = trackRepository;
        private readonly IRepository<Course> _courseRepository = courseRepository;

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
            var track = await _trackRepository.GetByIdWithNestedIncludesAsync(id, ["BranchTracks.Branch", "BranchTracks.Students", "CourseTracks.Course"]);
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
                }).ToList(),
                Courses = track.CourseTracks.Select(ct => new GetTrackInfoCourseDTO
                {
                    CourseId = ct.CourseId,
                    CourseName = ct.Course.Name
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

        //New

        public async Task<bool> UpdateTrackCourses(int id, List<int> ToBeAdded, List<int> ToBeRemoved)
        {

            if (ToBeAdded == null && ToBeRemoved == null)
                return false;
            var track = await _trackRepository.GetByIdAsync(id);
            if (track == null)
                return false;
            // Add new tracks
            foreach (var courseId in ToBeAdded)
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course != null)
                {
                    track.CourseTracks.Add(new CourseTrack { TrackId = track.Id, CourseId = courseId });
                }
            }
            // Remove courses
            foreach (var courseId in ToBeRemoved)
            {
                var TrackCourseToRemove = track.CourseTracks
                    .FirstOrDefault(ct => ct.TrackId == track.Id && ct.CourseId == courseId);

                if (TrackCourseToRemove != null)
                {
                    track.CourseTracks.Remove(TrackCourseToRemove);
                }
            }
            await _trackRepository.UpdateAsync(track);
            return true;
        }

        public async Task<IEnumerable<TrackCoursesSelectListDTO>> AllTracksWithTopicsForSelectList()
        {
            var tracks = await _trackRepository.GetAllWithNestedIncludesAsync("CourseTracks.Course");
            return tracks.Select(t => new TrackCoursesSelectListDTO
            {
                TrackId = t.Id,
                TrackName = t.Name,
                Courses = t.CourseTracks.Select(bt => new CourseSelectListDTO
                {
                    CourseId = bt.Course.Id,
                    CourseName = bt.Course.Name,
                }).ToList(),
            });
        }

        public async Task<TrackCoursesSelectListDTO> TrackWithCoursesSelectList(int id)
        {
            var track = await _trackRepository.GetByIdWithNestedIncludesAsync(id, "CourseTracks.Course");
            if (track == null)
                return null;
            var trackDto = new TrackCoursesSelectListDTO
            {
                TrackId = track.Id,
                TrackName = track.Name,
                Courses = track.CourseTracks.Select(bt => new CourseSelectListDTO
                {
                    CourseId = bt.Course.Id,
                    CourseName = bt.Course.Name,
                }).ToList(),
            };
            return trackDto;
        }

    }
}
