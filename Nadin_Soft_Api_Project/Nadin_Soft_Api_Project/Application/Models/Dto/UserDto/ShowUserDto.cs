namespace Nadin_Soft_Api_Project.Application.Models.Dto.UserDto
{
    public class ShowUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
