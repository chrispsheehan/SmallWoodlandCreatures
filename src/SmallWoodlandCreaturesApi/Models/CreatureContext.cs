using Microsoft.EntityFrameworkCore;

namespace SmallWoodlandCreaturesApi.Models
{
    public class CreatureContext : DbContext
    {
        public CreatureContext(DbContextOptions<CreatureContext> options)
            : base(options)
        {
        }

        public DbSet<Creature> Creatures { get; set; }
    }
}