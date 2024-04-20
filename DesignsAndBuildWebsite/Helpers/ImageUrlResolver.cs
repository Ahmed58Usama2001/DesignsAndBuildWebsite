namespace DesignsAndBuild.APIs.Helpers
{
    public class ImageUrlResolver : IValueResolver<OurProject, OurProjectReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public ImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
   

        public IEnumerable<string> Resolve(OurProject source, OurProjectReturnDto destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            var ImagesUrl = new List<string>();

            if (source.Imags.Count() > 0)
            {
                foreach (var item in source.Imags)
                {
                    ImagesUrl.Add($"{_configuration["ApiBaseUrl"]}/{item.PictureUrl}");
                }
                return ImagesUrl;
            }

            return new List<string>();
        }
    }
}
