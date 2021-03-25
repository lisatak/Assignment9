using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Models
{
    public class EFMoviesRepository : IMoviesRepository
    {
        private MoviesDbContext _context;

        //Constructor
        public EFMoviesRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public IQueryable<Movies> Movies => _context.Movies;
    }
}
