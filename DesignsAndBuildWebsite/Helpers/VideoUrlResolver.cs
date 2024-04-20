namespace DesignsAndBuild.APIs.Helpers
{
    public class VideoUrlResolver : IValueResolver<OurProject, OurProjectReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public VideoUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OurProject source, OurProjectReturnDto destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.VideoUrl)) 
           {
                return $"{_configuration["ApiBaseUrl"]}/{source.VideoUrl}";
           }

            return string.Empty;
        }
    }
}


