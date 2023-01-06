using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cube4solo.Models;

namespace Cube4solo.models
{
    public class Users
    {
        [Key] public int Id { get; set; }
        [StringLength(48)] public string Firstname { get; set; } = "";
        [StringLength(48)] public string Lastname { get; set; } = "";
        [StringLength(48)] public string Email { get; set; } = "";
        [StringLength(32)] public string Cellphone { get; set; } = "";
        [StringLength(32)] public string LandlinePhone { get; set; } = "";
        
        [ForeignKey("Services")] 
        public int ServiceId { get; set; }
        public Services Services { get; set; }
        
        [ForeignKey("Sites")]
        public int SiteId { get; set; }
        public Sites Sites { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}