using Core.Application;
using static Domain.Enums.Enums;

namespace Application.DomainEvents
{
    internal sealed class PlayerCreated : DomainEvent
    {
        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public int Age { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
