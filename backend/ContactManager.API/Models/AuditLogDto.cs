namespace ContactManager.API.Models
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
        public string ContactName { get; set; }
        public string Details { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
