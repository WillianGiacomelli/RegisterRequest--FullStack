using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace requestpAPI.Models;

public class Request : Base
{
    public DateTime RequestDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Observation { get; set; }
    public IEnumerable<RequestItem> Items { get; set; } = new List<RequestItem>();
}
