namespace FlowerShop.WebAPI
{
    public class Order
    {
        public Order(string typeOfFlower, int quantity, string orderType)
        {
            this.TypeOfFlower = typeOfFlower;
            this.Quantity = quantity;
            this.OrderType = orderType;
        }
        public string TypeOfFlower { get; set; }

        public int Quantity { get; set; }

        public string OrderType { get; set; }

    }
}
