namespace AuthorizeService.Dtos
{
    public class RolReadDto
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PermitionReadDto> Permitions { get; set; }
    }
}