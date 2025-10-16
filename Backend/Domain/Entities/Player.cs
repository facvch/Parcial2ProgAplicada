using Core.Domain.Entities;
using Domain.Validators;

namespace Domain.Entities
{
    public class Player : DomainEntity<string, PlayerValidator>
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
