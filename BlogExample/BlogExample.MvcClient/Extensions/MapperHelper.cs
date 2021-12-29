using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlogExample.MvcClient.Models;
using BlogExample.MvcClient.FilterModels;
using BlogExample.WebClientBL.Models;
using PagedList;

namespace BlogExample.MvcClient
{
    public static class MapperHelper
    {
        public static MapperConfiguration MapperConfig()
        {
            return new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, UserViewModel>()
                    .ForMember(s => s.Id, x => x.MapFrom(s => s.Id))
                    .ForMember(s => s.Nickname, x => x.MapFrom(s => s.Nickname));

                cfg.CreateMap<Blog, CreateBlogViewModel>();

                cfg.CreateMap<Blog, EditBlogViewModel>()
                    .ForMember(s => s.Author, x => x.MapFrom(s => s.User));

                cfg.CreateMap<Blog, BlogSimpleViewModel>()
                    .ForMember(s => s.Author, x => x.MapFrom(s => s.User));

                cfg.CreateMap<BlogFilter, BlogFilterViewModel>();

                cfg.CreateMap<BlogFilterViewModel, BlogFilter>();

                cfg.CreateMap<Comment, CommentViewModel>()
                    .ForMember(s => s.Commenter, x => x.MapFrom(s => s.User))
                    .ForMember(s => s.BlogId, x => x.MapFrom(s => s.Blog.Id));

                cfg.CreateMap<Blog, BlogDetailViewModel>()
                    .ForMember(s => s.Author, x => x.MapFrom(s => s.User))
                    .ForMember(s => s.Comments, x => x.Ignore());

                cfg.CreateMap<Blog, BlogHomeViewModel>()
                    .ForMember(s => s.Author, x => x.MapFrom(s => s.User))
                    .ForMember(s => s.LastComment, x => x.MapFrom(s =>
                            s.Comments.OrderByDescending(z => z.Created).FirstOrDefault()
                        ));
            });
        }
    }
}