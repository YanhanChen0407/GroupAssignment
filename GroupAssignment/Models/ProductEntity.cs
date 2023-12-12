namespace GroupAssignment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int ProductQuantity { get; set; }

        public string ProductDescription { get; set; }
    }
}
