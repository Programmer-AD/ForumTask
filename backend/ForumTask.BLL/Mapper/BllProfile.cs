using AutoMapper;
using ForumTask.BLL.DTO;
using ForumTask.DAL.Entities;

namespace ForumTask.BLL.Mapper
{
    public class BllProfile : Profile
    {
        public BllProfile()
        {
            CreateMap<Mark, MarkDto>()
                .ReverseMap();

            CreateMap<Message, MessageDto>()
                .ReverseMap();

            CreateMap<Topic, TopicDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
