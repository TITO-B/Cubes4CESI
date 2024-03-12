using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Salarie
    {
        //Propriétés
        [Key]
        public int SalarieId { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        public string TelephoneFixe { get; set; }

        public string TelephonePortable { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Relation avec la table Services
        [ForeignKey("Service")]
        [Required]
        public int ServiceId { get; set; }

        // Relation avec la table Sites
        [ForeignKey("Site")]
        [Required]
        public int SiteId { get; set; }


    }

}
