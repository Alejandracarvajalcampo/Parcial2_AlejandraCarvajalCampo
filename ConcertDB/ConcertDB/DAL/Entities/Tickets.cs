using System.ComponentModel.DataAnnotations;

namespace ConcertDB.DAL.Entities
{
    public class Tickets
    {
        [Key]
        [Required]
        [Display(Name = "Codigo")]
        public Guid Id { get; set; }

        [Display(Name = "Fecha")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "Ingreso")]
        public Boolean IsUsed { get; set; }

        [Display(Name = "Localidad")]
        public String? EntranceGate { get; set; }
        

    }
}
