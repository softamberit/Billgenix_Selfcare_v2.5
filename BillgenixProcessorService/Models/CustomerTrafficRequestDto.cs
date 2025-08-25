namespace TrafficProcessorService.Models;

public class CustomerTrafficRequestDto
{
    public int Id { get; set; }
    public int RouterId { get; set; }
    public string CustomerID { get; set; }
    public string MkUser { get; set; }
    public DateTime EntryDate { get; set; }
    public int TrafficDurationInMenutes { get; set; }
    public int ProcessStatus { get; set; }   // 0 = Pending, 1 = Processed (example)
    public DateTime? ProcessedTime { get; set; }  // nullable because it can be NULL

    public string? ConnectionId { get; set; }
}

public class UpdateTrafficRequestDto
{
    public int Id { get; set; }

    public int ProcessStatus { get; set; }   // 0 = Pending, 1 = Processed (example)

    public int AvgRx { get; set; }  // Average received traffic in bytes per second
    public int AvgTx { get; set; }  // Average received traffic in bytes per second


}
