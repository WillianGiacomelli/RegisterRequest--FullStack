using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace requestpAPI.Models;

public class RequestItem : Base
{
    [Required]
    public string ProductCode { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal ProductValue { get; set; }

    public int RequestId { get; set; }

    [JsonIgnore]
    public Request? Request { get; set; }
}
