namespace FlowerShop.WebAPI
{
    public class Order
    {
        public int Id { get; set; }
        public string FlowerType { get; set; }

        public int Quantity {  get; set; }
        public int OrderTypeId {  get; set; }

        public bool IsActive{ get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int CreatedByUserId { get; set; }

        public int UpdatedByUserId { get; set; }


    }
}
