namespace SqlRetryAdventures.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SqlContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SqlRetryAdventures.SqlContext";
        }

        protected override void Seed(SqlContext context)
        {
            context.Users.AddOrUpdate(new User
            {
                UserId = 1,
                Email = "dustin.dahl@gmail.com",
                Password = "fghwgads"
            });

            context.Posts.AddOrUpdate(new Post
            {
                PostId = 1,
                UserId = 1,
                Title = "Title",
                Content = "this is some content"
            });

            context.SaveChanges();
        }
    }
}
