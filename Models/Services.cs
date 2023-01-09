using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cube4solo.Models
{
    public class Services
    {
        [Key] public int? Id { get; set; }
        [StringLength(48)] public string Name { get; set; } = "";
    }
}