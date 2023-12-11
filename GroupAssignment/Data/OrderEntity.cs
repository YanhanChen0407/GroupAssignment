using GroupAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace GroupAssignment.Data.Entities
{
    public class OrderEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string MFRName { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; } = "In Progress";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime OrderDeliveryDate { get; set; } = DateTime.Now.AddDays(10);
        [Required]
        public UserModel User { get; set; }
        [Required]
        public List<ProductModel> Products { get; set; }
    }
}
