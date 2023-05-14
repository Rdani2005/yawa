namespace AuthService.Dtos
{
    public class UserReadDto
    {

        public int Id { get; set; }
        public String Identification { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime CreatedDay { get; set; }
        public RolReadDto UserRole { get; set; }
    }
}