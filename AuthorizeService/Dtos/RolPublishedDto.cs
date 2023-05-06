namespace AuthorizeService.Dtos
{
    public class RolPublishedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PermitionPublishedDto> Permitions { get; set; }
    }
}