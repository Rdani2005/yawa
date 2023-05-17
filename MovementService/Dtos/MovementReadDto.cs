namespace MovementService.Dtos
{
    public class MovementReadDto
    {
        public int Id { get; set; }
        public float MovementAmount { get; set; }
        public String MovementDescription { get; set; }
        public TypeReadDto Type { get; set; }
        public DateTime DoneDate { get; set; }
    }
}