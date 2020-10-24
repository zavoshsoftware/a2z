using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BlogDetailViewModel:BaseViewModel
    {
        public Blog blog { get; set; }
        public List<BlogCategory> Categories { get; set; }
        public List<Blog> Recent { get; set; }
        public List<Blog> Related { get; set; }
        public List<BlogComment> Comments { get; set; }
    }
    public class BlogCategory
    {
        public BlogGroup BlogGroup { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}