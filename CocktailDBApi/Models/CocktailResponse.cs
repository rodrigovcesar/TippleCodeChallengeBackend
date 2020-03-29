using System.Collections.Generic;

namespace Models
{
    public class CocktailResponse
    {
        public IEnumerable<CocktailAllProperties> Drinks { get; set; }
    }
}
