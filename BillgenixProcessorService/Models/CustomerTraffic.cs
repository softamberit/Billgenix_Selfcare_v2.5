namespace BillgenixProcessorService.Models
{
    public class CustomerTraffic
    {
        public string Name { get; set; }
        public float Tx { get; set; }
        public float Rx { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
