namespace CrudApplication.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Action {  get; set; }
        public DateTime TimeStap {  get; set; }
        public int UserId { get; set; }
        public string IP {  get; set; }

        public bool Success { get; set; }
    }
}
