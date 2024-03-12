using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Service
    {
        //Propriétés
        [Key]
        public int ServiceId { get; set; }

        [Required]
        public string Nom { get; set; }
    }

}
