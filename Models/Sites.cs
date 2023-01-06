using System.ComponentModel.DataAnnotations;

namespace Cube4solo.Models
{
    public class Sites
    {
        [Key] public int Id { get; set; }
        [StringLength(48)] public string City { get; set; } = "";
    }
}

