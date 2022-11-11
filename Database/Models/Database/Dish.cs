namespace Database.Models.Database
{
    public partial class Dish
    {
        public int DishId { get; set; }
        public string DishName { get; set; } = null!;
        public string MainCategory { get; set; } = null!;
        public int MakeTimeMin { get; set; }
        public string PreparationDifficulty { get; set; } = null!;
        public string IngredientsCategory { get; set; } = null!;
        public string IngredientsTags { get; set; } = null!;
        public string? DishRecipeUrl { get; set; }
    }
}
