using Microsoft.AspNetCore.Builder;
using Nadin_Soft_Api_Project.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nadin_Soft_Api_Project.Domain.Entities.Product;

public class Product : BaseEntity
{
    public int UserRef { get; set; }
    public User.User User { get; set; }
    public string ManufactureEmail { get; set; }
    public string ManufacturePhone { get; set; }
    public DateTime ProduceDate { get; set; } = DateTime.Now;
    public string Name { get; set; }
}
