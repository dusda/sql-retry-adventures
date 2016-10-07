using SqlRetryAdventures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlRetryAdventures.UI
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            //run these two concurrently
            var holdTask = HoldUpDb();
            var insertTask = InsertStuff();
            await Task.WhenAll(holdTask, insertTask);

            Console.ReadKey();
        }

        static async Task InsertStuff()
        {
            Console.WriteLine("Adding stuff to the db...");

            using (var context = new SqlContext())
            {
                //set timeout shorter than the hold up will run
                context.Database.CommandTimeout = 2;

                //make some stuff...
                var user = context.Users.Create();
                user.UserId = 2;
                user.Password = "fajdsfk";
                user.Email = "bleh@fghwgads.com";

                for (int i = 0; i < 3; i++)
                {
                    var post = context.Posts.Create();
                    post.UserId = 2;
                    post.Title = $"{i}";
                    post.Content = RandomString(500);
                    user.Posts.Add(post);

                    context.SaveChanges();

                    foreach (var item in user.Posts)
                    {
                        var com = context.Comments.Create();
                        com.PostId = item.PostId;
                        com.Content = RandomString(100);
                        item.Comments.Add(com);
                    }
                }

                context.Users.Add(user);

                //and try to save it.
                //the retry policy will execute behind the scenes
                //here. See the AwesomeConfiguration class.
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// This will basically lock the db up for a few seconds,
        /// for testing purposes.
        /// </summary>
        /// <returns></returns>
        static async Task HoldUpDb()
        {
            var con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["SqlContext"].ConnectionString);

            var cmd = new SqlCommand(
                @"  BEGIN TRANSACTION
                    SELECT * FROM [Comments] WITH (TABLOCKX, HOLDLOCK)
                    WHERE 0 = 1
                    WAITFOR DELAY '00:00:05'
                    ROLLBACK TRANSACTION");

            cmd.Connection = con;
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            con.Close();
        }

        static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
