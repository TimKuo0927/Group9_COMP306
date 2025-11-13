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

            CreateMap<BookCategory,GetBookCategoryDto>();
            CreateMap<BookCategory, PostBookCategoryDto>();
            CreateMap<PostBookCategoryDto, BookCategory>();

            CreateMap<Book, GetBookDto>();
            CreateMap<Book, PostBookDto>();
            CreateMap<PostBookDto, Book>();
        }
    }
}
