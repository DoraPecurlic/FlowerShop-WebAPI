namespace FlowerShop.WebAPI.Models
{
    public class OrderType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public bool isActive {  get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }

    }
}
