using AutoMapper;
using WebAPIClone.Data;
using WebAPIClone.Model;

namespace WebAPIClone.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<BookCreateModel, Book>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<ApplicationUser, AccountModel>().ReverseMap();
        }
    }
}
