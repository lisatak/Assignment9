using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Models.ViewModels
{
    public class MoviesListViewModel
    {
        //List of all the movie objects
        public IEnumerable<Movies> Movies { get; set; }
    }
}
