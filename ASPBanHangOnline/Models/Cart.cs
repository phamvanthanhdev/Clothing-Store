namespace ASPBanHangOnline.Models
{
    public class Cart
    {
        public List<CartLine>? CartLines { get; set; }=new List<CartLine>();
        public void AddItem(Product product, int quantity)
        {
            CartLine? line = CartLines.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                CartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            CartLine? line = CartLines.Where(p => p.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line != null)
            {
                CartLines.Remove(line);
            }
        }

        public decimal ComputeTotalPrice() => CartLines.Sum(l => l.Product.ProductPrice * (1 - l.Product.ProductDiscount) * l.Quantity);

        public void ClearAll() => CartLines.Clear();
    }

    public class CartLine
    {
        public int CartId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
