using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Site
    {
        //Propriétés
        [Key]
        public int SiteId { get; set; }

        [Required]
        public string Ville { get; set; }
    }

}
