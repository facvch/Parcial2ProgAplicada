using Core.Domain.Entities;
using Domain.Validators;

namespace Domain.Entities
{
    public class Attempt : DomainEntity<int, AttemptValidator>
    {
        public int AttemptId { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string AttemptedNumber { get; set; }
        public DateTime AttemptDate { get; set; }
        public string Result { get; set; }
    }
}
