public class Attempt
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public string AttemptedNumber { get; set; } = null!; // "2564"
    public string Message { get; set; } = null!;         // "Tu número tiene 1 fama y 2 pica"
    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

    public Game Game { get; set; } = null!;
}
