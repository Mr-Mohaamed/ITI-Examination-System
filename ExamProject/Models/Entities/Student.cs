namespace ExamProject.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }

        //Age taken from DOB
        public int Age
        {
            get
            {
                // Check if the birthday has occurred this year
                if (DOB > DateTime.Now.AddYears(-1))
                {
                    return DateTime.Now.Year - DOB.Year - 1;
                }
                else
                {
                    return DateTime.Now.Year - DOB.Year;
                }
            }
        }
        public int BranchId { get; set; }
        public int TrackId { get; set; }

        // Navigation properties

        // Student --- BranchTrack
        public BranchTrack BranchTrack { get; set; }
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
