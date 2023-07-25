namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string username { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string username)
        {
            this.username = username;
        }

        public decimal TotalPrice
        {
            get
            {
                var totalPrice = 0m;
                foreach (var item in Items)
                {
                    totalPrice += item.Quanlity * item.Price;
                }

                return totalPrice;
            }
        }
    }
}
