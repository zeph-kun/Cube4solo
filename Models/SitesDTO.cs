using System.ComponentModel.DataAnnotations;

namespace Cube4solo.Models
{
    public class SitesDTO
    {
        public int Id { get; set; }
        [Required] public string City { get; set; } = "";
    }
}

