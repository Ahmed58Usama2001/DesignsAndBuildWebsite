namespace DesignsAndBuild.APIs.Dtos.ProjectDtos
{
    public class OurProjectDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ArabicTitle { get; set; }
        [Required]
        public string? ClientName { get; set; }
        [Required]
        public string? ArabicClientName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ArabicDescription { get; set; }
        [Required]
        public string? Duration { get; set; }
        [Required]
        public List<IFormFile> Image { get; set; }
        [Required]
        public string ArabicDuration { get; set; }

        public IFormFile? Video { get; set; }

    }
}
