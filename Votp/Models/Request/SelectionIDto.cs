namespace Votp.Models.Request
{
    public class SelectionIDto
    {
        public IEnumerable<int> Selection { get; set; }
        public string Action { get; set; }
    }
}
