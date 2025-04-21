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

            modelBuilder.Entity<Branch>()
                .HasMany(c => c.BranchTracks)
                .WithOne(ct => ct.Branch)
                .HasForeignKey(ct => ct.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>()
                .HasMany(c => c.BranchTracks)
                .WithOne(ct => ct.Track)
                .HasForeignKey(ct => ct.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.BranchTrack)
                .WithMany(bt => bt.Students)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BranchTrack>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.TrackId });

                entity.HasMany(bt => bt.Students)
                      .WithOne(s => s.BranchTrack)
                      .HasForeignKey(s => new { s.BranchId, s.TrackId })
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Branch)
                      .WithMany(b => b.BranchTracks)
                      .HasForeignKey(e => e.BranchId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(e => e.Track)
                      .WithMany(t => t.BranchTracks)
                      .HasForeignKey(e => e.TrackId)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(e => e.Students)
                        .WithOne(s => s.BranchTrack)
                        .HasForeignKey(e => new { e.BranchId, e.TrackId });

            });

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseTracks)
                .WithOne(ct => ct.Course)
                .HasForeignKey(ct => ct.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>()
                .HasMany(c => c.CourseTracks)
                .WithOne(ct => ct.Track)
                .HasForeignKey(ct => ct.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseTrack>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.TrackId });

                entity.HasOne(e => e.Track)
                      .WithMany(t => t.CourseTracks)
                      .HasForeignKey(e => e.TrackId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Course)
                      .WithMany(t => t.CourseTracks)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Course>()
                .HasMany(c => c.CourseTopics)
                .WithOne(ct => ct.Course)
                .HasForeignKey(ct => ct.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Topic>()
               .HasMany(c => c.CourseTopics)
               .WithOne(ct => ct.Topic)
               .HasForeignKey(ct => ct.TopicId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseTopic>(entity =>
            {
                entity.HasKey(e => new { e.TopicId, e.CourseId });

                entity.HasOne(e => e.Topic)
                      .WithMany(t => t.CourseTopics)
                      .HasForeignKey(e => e.TopicId)
                      //.OnDelete(DeleteBehavior.Restrict); // <== Prevent cascade
                      .OnDelete(DeleteBehavior.Cascade); // <== Prevent cascade


                entity.HasOne(e => e.Course)
                      .WithMany(t => t.CourseTopics)
                      .HasForeignKey(e => e.CourseId)
                      //.OnDelete(DeleteBehavior.Restrict); // <== Prevent cascade
                      .OnDelete(DeleteBehavior.Cascade); // <== Prevent cascade
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

            modelBuilder.Entity<Exam>()
               .HasMany(e => e.ExamQuestions)
               .WithOne(eq => eq.Exam)
               .HasForeignKey(eq => eq.ExamId)
               .OnDelete(DeleteBehavior.Cascade); // ✅ Cascade delete Exam → ExamQuestions

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Exams)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Cascade delete Student → Exams

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Choices)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.HasKey(e => new { e.ExamId, e.QuestionId });

                entity.HasOne(e => e.Exam)
                      .WithMany(e => e.ExamQuestions)
                      .HasForeignKey(e => e.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);
                //.OnDelete(DeleteBehavior.Restrict);

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

//Student - Exam - Exam Questions
//Course - courseTopics - Topic