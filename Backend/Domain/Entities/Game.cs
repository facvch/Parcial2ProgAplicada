using Domain.Entities;

public class Game
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string SecretNumber { get; set; } = null!; // "5604"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsFinished { get; set; } = false;

    public Player Player { get; set; } = null!;
    public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}
