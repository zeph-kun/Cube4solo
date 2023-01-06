using System.ComponentModel.DataAnnotations;

namespace Cube4solo.Models
{
    public class ServicesDTO
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = "";
    }
}