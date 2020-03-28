using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FilteredResponse
    {
        public IEnumerable<CocktailSummary> Drinks { get; set; }
    }
}
