namespace WebApi.Models.Dto
{
    public class MovieInput
    {
        required public string Title { get; set; }
        public DateTime? Date { get; set; }
        required public string Name { get; set; }
        required public string SureName { get; set; }
    }
}
