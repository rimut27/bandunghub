using System.ComponentModel.DataAnnotations;

namespace BandungHub.Models
{
    public class Departemen
    {
        public int Id { get; set; } // ID akan auto-increment

        [Required]
        public string Nama { get; set; }

        [Required]
        [Url]
        public string Link { get; set; }
        

        public string? Icon { get; set; }
    }
}
