using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CocktailAllProperties
    {
        public int IdDrink { get; set; }
        public string StrDrink { get; set; }
        public string StrDrinkAlternate { get; set; }
        public string StrDrinkES { get; set; }
        public string StrDrinkDE { get; set; }
        public string StrDrinkFR { get; set; }

        [JsonProperty("strDrinkZH-HANT")]
        public string StrDrinkZH_HANS { get; set; }
        public string StrTags { get; set; }
        public string StrVideo { get; set; }
        public string StrCategory { get; set; }
        public string StrIBA { get; set; }
        public string StrAlcoholic { get; set; }
        public string StrGlass { get; set; }
        public string StrInstructions { get; set; }
        public string StrInstructionsES { get; set; }
        public string StrInstructionsDE { get; set; }
        public string StrInstructionsFR { get; set; }
        [JsonProperty("strInstructionsZH-HANS")]
        public string strInstructionsZH_HANS { get; set; }
        [JsonProperty("strInstructionsZH-HANT")]
        public string StrInstructionsZH_HANT { get; set; }
        public string StrDrinkThumb { get; set; }
        public string StrIngredient1 { get; set; }
        public string StrIngredient2 { get; set; }
        public string StrIngredient3 { get; set; }
        public string StrIngredient4 { get; set; }
        public string StrIngredient5 { get; set; }
        public string StrIngredient6 { get; set; }
        public string StrIngredient7 { get; set; }
        public string StrIngredient8 { get; set; }
        public string StrIngredient9 { get; set; }
        public string StrIngredient10 { get; set; }
        public string StrIngredient11 { get; set; }
        public string StrIngredient12 { get; set; }
        public string StrIngredient13 { get; set; }
        public string StrIngredient14 { get; set; }
        public string StrIngredient15 { get; set; }
        public string StrMeasure1 { get; set; }
        public string StrMeasure2 { get; set; }
        public string StrMeasure3 { get; set; }
        public string StrMeasure4 { get; set; }
        public string StrMeasure5 { get; set; }
        public string StrMeasure6 { get; set; }
        public string StrMeasure7 { get; set; }
        public string StrMeasure8 { get; set; }
        public string StrMeasure9 { get; set; }
        public string StrMeasure10 { get; set; }
        public string StrMeasure11 { get; set; }
        public string StrMeasure12 { get; set; }
        public string StrMeasure13 { get; set; }
        public string StrMeasure14 { get; set; }
        public string StrMeasure15 { get; set; }
        public string StrCreativeCommonsConfirmed { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
