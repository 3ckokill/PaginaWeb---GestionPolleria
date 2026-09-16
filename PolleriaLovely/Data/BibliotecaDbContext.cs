
using Microsoft.EntityFrameworkCore;

namespace PolleriaLovely.Data
{
    public class BibliotecaDbContext:DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext>options): base(options)
        {

        }

        
    }
}
