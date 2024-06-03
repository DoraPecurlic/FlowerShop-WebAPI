namespace FlowerShop.WebAPI
{
    public class Order
    {
        public Guid Id { get; set; }
        public string FlowerType { get; set; }

        public int Quantity {  get; set; }
        public Guid OrderTypeId {  get; set; }

        public bool IsActive{ get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }


    }
}
