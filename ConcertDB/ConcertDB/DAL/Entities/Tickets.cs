using System.ComponentModel.DataAnnotations;

namespace ConcertDB.DAL.Entities
{
    public class Tickets
    {
        [Key]
        [Required]
        [Display(Name = "Tiquetes Bichota")]
        public Guid Id { get; set; }    
        public DateTime? UseDate { get; set; }
        public Boolean IsUsed { get; set; }
        public String? EntranceGate { get; set; }
        




    }
}
