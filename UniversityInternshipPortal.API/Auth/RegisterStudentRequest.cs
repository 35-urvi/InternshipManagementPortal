namespace UniversityInternshipPortal.API.Auth
{
    public class RegisterStudentRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Department { get; set; }

        public double CGPA { get; set; }
    }
}
