namespace FlowerShop.WebAPI.Models
{
    public class OrderType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public bool isActive {  get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }

    }
}
