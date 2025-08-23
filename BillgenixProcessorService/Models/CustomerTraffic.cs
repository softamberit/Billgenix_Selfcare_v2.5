namespace BillgenixProcessorService.Models
{
    public class CustomerTraffic
    {
        public string Name { get; set; }
        public float Tx { get; set; }
        public float Rx { get; set; }
        public DateTime Timestamp { get; set; }
        public int AvgRx { get; set; }  // Average received traffic in bytes per second
        public int AvgTx { get; set; }  // Average received traffic in bytes per second
    }
}
