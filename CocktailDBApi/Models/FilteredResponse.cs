using System.Collections.Generic;

namespace Models
{
    public class FilteredResponse
    {
        public IEnumerable<CocktailSummary> Drinks { get; set; }
    }
}
