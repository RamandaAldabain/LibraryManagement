using AutoMapper;
using LibraryManagement.Dto;

namespace LibraryManagement.Web.Host.Startup
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Models.Task, TaskDto>().ReverseMap();

		}
	}
}