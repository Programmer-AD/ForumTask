using AutoMapper;
using ForumTask.BLL.DTO;
using ForumTask.PL.Models;

namespace ForumTask.PL.Mapper
{
    public class PlProfile : Profile
    {
        public PlProfile()
        {
            CreateMap<MessageAddModel, MessageDto>();
            CreateMap<MessageDto, MessageViewModel>();

            CreateMap<TopicDto, TopicViewModel>();

            CreateMap<UserDto, UserViewModel>();
        }
    }
}
