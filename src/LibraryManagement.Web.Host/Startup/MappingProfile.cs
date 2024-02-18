using AutoMapper;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.Dto;
using LibraryManagement.Roles.Dto;
using LibraryManagement.Users.Dto;

namespace LibraryManagement.Web.Host.Startup
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Entities.Task, TaskDto>().ReverseMap();
			CreateMap<Role, CreateRoleDto>().ReverseMap();
			CreateMap<Role, RoleDto>().ReverseMap();
			CreateMap<Role, RoleEditDto>().ReverseMap();
			CreateMap<Role, RoleListDto>().ReverseMap();
			CreateMap<User, CreateUserDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();


		}
	}
}