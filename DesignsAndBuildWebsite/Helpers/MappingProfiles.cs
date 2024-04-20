namespace DesignsAndBuild.APIs.Helpers;

public class MappingProfiles : Profile
{
        public MappingProfiles()
        {
            
            CreateMap<CustomerMessageDetails,CustomerMessageDetailsDto>().ReverseMap();
            CreateMap<OurProjectDto, OurProject>().ReverseMap();
            CreateMap<OurProject, OurProjectReturnDto>()
            .ForMember(e => e.VideoUrl, O => O.MapFrom<VideoUrlResolver>())
            .ForMember(e=>e.ImageUrl,O => O.MapFrom<ImageUrlResolver>())
            .ReverseMap();

            CreateMap<OurProject, OurProjectUpdatedDto>().ReverseMap();
           
        }
}