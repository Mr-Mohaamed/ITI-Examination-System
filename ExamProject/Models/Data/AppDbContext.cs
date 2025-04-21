using ExamProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchTrack> BranchTracks { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; } //instructor
        public DbSet<CourseTrack> CourseTracks { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<CourseTopic> CoursesTopic { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ExamQuestionAnswer>(entity =>
            //{
            //    entity.HasKey(e => new { e.ExamId, e.QuestionId });

            //    //entity.Property(e => e.ExamId).HasColumnName("E_ID");
            //    //entity.Property(e => e.QuestionId).HasColumnName("Q_ID");
            //    //entity.Property(e => e.StudentAnswerId).HasColumnName("Student_Answer");

            //    entity.HasOne(e => e.Exam)
            //          .WithMany(e => e.ExamQuestionAnswers)
            //          .HasForeignKey(e => e.ExamId);

            //    entity.HasOne(e => e.Question)
            //          .WithMany(q => q.ExamQuestionAnswers)
            //          .HasForeignKey(e => e.QuestionId);

            //    entity.HasOne(e => e.StudentAnswer)
            //          .WithMany(c => c.ExamQuestionAnswers)
            //          .HasForeignKey(e => e.StudentAnswerId);
            //});
            modelBuilder.Entity<BranchTrack>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.TrackId });

                entity.HasOne(e => e.Branch)
                      .WithMany(b => b.BranchTracks)
                      .HasForeignKey(e => e.BranchId);

                entity.HasOne(e => e.Track)
                      .WithMany(t => t.BranchTracks)
                      .HasForeignKey(e => e.TrackId);

                entity.HasMany(e => e.Students)
                        .WithOne(s => s.BranchTrack)
                        .HasForeignKey(e => new { e.BranchId, e.TrackId });
            });
            modelBuilder.Entity<CourseTrack>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.TrackId });

                entity.HasOne(e => e.Track)
                      .WithMany(t => t.CourseTracks)
                      .HasForeignKey(e => e.TrackId);

                entity.HasOne(e => e.Course)
                      .WithMany(t => t.CourseTracks)
                      .HasForeignKey(e => e.CourseId);
            });
            modelBuilder.Entity<CourseTopic>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.CourseId });

                entity.HasOne(e => e.Topic)
                      .WithMany(t => t.CourseTopics)
                      .HasForeignKey(e => e.TopicId)
                      .OnDelete(DeleteBehavior.Restrict); // <== Prevent cascade


                entity.HasOne(e => e.Course)
                      .WithMany(t => t.CourseTopics)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Restrict); // <== Prevent cascade


            });
            modelBuilder.Entity<CourseAssignment>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.TrackId, e.BranchId });

                entity.HasOne(e => e.CourseTrack)
                      .WithMany(t => t.CourseAssignments)
                      .HasForeignKey(e => new { e.TrackId, e.CourseId });

                entity.HasOne(e => e.Branch)
                        .WithMany(t => t.CourseAssignments)
                        .HasForeignKey(e => e.BranchId);

                entity.HasOne(e => e.Instructor)
                        .WithMany(t => t.CourseAssignments)
                        .HasForeignKey(e => e.InstructorId);
            });
            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.HasKey(e => new { e.ExamId, e.QuestionId });

                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.ExamQuestions)
                      .HasForeignKey(e => e.ExamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Question)
                      .WithMany(q => q.ExamQuestions)
                      .HasForeignKey(e => e.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.StudentAnswer)
                      .WithMany(c => c.ExamQuestions)
                      .HasForeignKey(e => e.StudentAnswerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
