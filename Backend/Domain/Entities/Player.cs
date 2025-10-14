using System;

namespace Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public int Age { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
