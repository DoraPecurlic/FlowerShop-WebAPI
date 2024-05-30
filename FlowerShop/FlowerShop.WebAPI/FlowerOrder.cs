namespace FlowerShop.WebAPI
{
    public class FlowerOrder
    {
        public FlowerOrder(string typeOfFlower, int quantity, string orderType)
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
