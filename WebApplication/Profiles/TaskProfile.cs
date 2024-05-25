using AutoMapper;
using WebApplication.DTOs;

namespace WebApplication.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Models.Task, TaskDTO>();
            CreateMap<TaskDTO, Models.Task>();
        }
    }
}