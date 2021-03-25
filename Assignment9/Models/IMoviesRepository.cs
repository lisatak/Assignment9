using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Models
{
    public interface IMoviesRepository
    {
        IQueryable<Movies> Movies { get; }
    }
}
