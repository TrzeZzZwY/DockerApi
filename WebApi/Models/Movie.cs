namespace WebApi.Models
{
    public class Movie : IIdentity<int>
    {
        public int Id { get; set; }
        required public string Title { get; set; }
        required public Director Director { get; set; }
        public DateTime? Date { get; set; }

    }
}
