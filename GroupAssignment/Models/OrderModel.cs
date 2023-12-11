using static System.Net.Mime.MediaTypeNames;

namespace GroupAssignment.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string MFRName { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; } = "Progress";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime OrderDeliveryDate { get; set; } = DateTime.Now.AddDays(10);
        public UserModel User { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
