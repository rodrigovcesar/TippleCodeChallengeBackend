using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CocktailResponse
    {
        public IEnumerable<CocktailAllProperties> Drinks { get; set; }
    }
}
