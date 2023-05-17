namespace MovementService.Dtos
{
    public class MovementPublishedDto
    {
        public int MovementId { get; set; }
        public float MovementAmount { get; set; }
        public int AccountId { get; set; }
        public String Event { get; set; }
    }
}