using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DammioCodeFirstNewDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                //
                Console.Write("Ten cua 1 blog moi: ");
                var name = Console.ReadLine();

                //
                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();

                //
                var query = from b in db.Blogs orderby b.Name select b;

                Console.WriteLine("Tat ca blog trong database: ");
                foreach (var item in query) Console.WriteLine(item.Name);
                Console.WriteLine("Go bat ky phim nao de thoat");
                Console.ReadKey();
            }
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url {  get; set; } // new
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.DisplayName).HasColumnName("display_name");
        }
    }
}
