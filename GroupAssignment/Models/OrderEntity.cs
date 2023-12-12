using System.ComponentModel.DataAnnotations;

namespace GroupAssignment.Models
{
    public class OrderEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string MFRName { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; } = "In Progress";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime OrderDeliveryDate { get; set; } = DateTime.Now.AddDays(10);
        [Required]
        public List<ProductEntity> Products { get; set; }
    }
}
