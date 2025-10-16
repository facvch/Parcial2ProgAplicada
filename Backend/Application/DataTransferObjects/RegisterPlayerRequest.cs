using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects
{
    public class RegisterPlayerRequest
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }
    }
}
