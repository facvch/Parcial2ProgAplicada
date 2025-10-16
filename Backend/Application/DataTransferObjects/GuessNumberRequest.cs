using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects
{
    public class GuessNumberRequest
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El número debe tener exactamente 4 dígitos")]
        public string AttemptedNumber { get; set; }
    }
}
