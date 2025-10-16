using Core.Domain.Entities;
using Domain.Validators;

namespace Domain.Entities
{
    public class Game : DomainEntity<string, GameValidator>
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public string SecretNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFinished { get; set; }
        public List<Attempt> Attempts { get; set; } = new List<Attempt>();
    }
}
