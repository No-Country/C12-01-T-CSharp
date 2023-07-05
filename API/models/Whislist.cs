namespace API.models
{
    public partial class Whislist
    {
        public string WishlistId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
