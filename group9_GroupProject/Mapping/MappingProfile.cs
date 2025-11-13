using AutoMapper;
using group9_GroupProject.DTO;
using group9_GroupProject.Models;
using System.Runtime;

namespace group9_GroupProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Publisher, GetPublisherDto>();//map from Publisher to GetPublisherDto
            CreateMap<PostPublisherDto, Publisher>();
            CreateMap<Publisher, PostPublisherDto>(); 
            CreateMap<PutPublisherDto, Publisher>();

            CreateMap<BookCategory,GetBookCategoryDto>();
            CreateMap<BookCategory, PostBookCategoryDto>();
            CreateMap<PostBookCategoryDto, BookCategory>(); 
            CreateMap<PutBookCategoryDto, BookCategory>();

            CreateMap<Book, GetBookDto>();
            CreateMap<Book, PostBookDto>();
            CreateMap<PostBookDto, Book>();
            CreateMap<Book, GetBookDetailDto>()
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.PublisherName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

        }
    }
}
