namespace GroupAssignment.Models
{
    using System;


    public class ProductModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductDescription { get; set; }
    }
}
