using Nadin_Soft_Api_Project.Domain.Entities.Common;

namespace Nadin_Soft_Api_Project.Domain.Entities.User;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public string Role { get; set; } = "User";
    public IList<Product.Product>? Products { get; set; }

}
