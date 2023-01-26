using AutoMapper;
using ObjectManagerBackend.Application.DTOs.AppObject.Request;
using ObjectManagerBackend.Application.DTOs.AppObject.Response;
using ObjectManagerBackend.Domain.Models.AppObject;

namespace ObjectManagerBackend.Application.Mapping
{
    /// <summary>
    /// AppObject mapper profile
    /// </summary>
    public class AppObjectProfile : Profile
    {
        /// <summary>
        /// Constructor: Creates a new instance of <see cref="AppObjectProfile"/>
        /// </summary>
        public AppObjectProfile()
        {
            CreateMap<AppObjectCreateRequest, AppObjectModel>();
            CreateMap<AppObjectUpdateRequest, AppObjectModel>();
            CreateMap<AppObjectModel, AppObjectResponse>();
        }
    }
}
