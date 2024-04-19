namespace DesignsAndBuild.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles( )
        {
            
            CreateMap<CustomerMessageDetails,CustomerMessageDetailsDto>().ReverseMap(); 
        }
    }
}