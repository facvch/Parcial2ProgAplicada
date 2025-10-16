using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects
{
    public class StartGameRequest
    {
        [Required]
        public int PlayerId { get; set; }
    }
}
