using Products;

namespace Domain.Entities
{
    public class OrderItem 
    {
        public string Id {get; set; }
        public IProduct Product { get; set; }
        
        public int Quantity { get; set; }

        public decimal Total { get { return Product.Price * Quantity;  }}

       
    }
}