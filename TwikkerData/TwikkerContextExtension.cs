using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Twikker.Data.Models;
using System.Collections.Generic;

namespace Twikker.Data
{
    public static class TwikkerContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this TwikkerContext context)
        {
            if (!context.Users.Any())
            {
                var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "Users.json"));
                context.AddRange(users);

                var userTexts = JsonConvert.DeserializeObject<List<UserText>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "UserTexts.json"));
                context.AddRange(userTexts);

                var posts = JsonConvert.DeserializeObject<List<Post>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "Posts.json"));
                context.AddRange(posts);

                var comments = JsonConvert.DeserializeObject<List<Comment>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "Comments.json"));
                context.AddRange(comments);

                var reactions = JsonConvert.DeserializeObject<List<Reaction>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "Reactions.json"));
                context.AddRange(reactions);

                context.SaveChanges();
            }
        }
    }
}
