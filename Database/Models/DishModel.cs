namespace Database.Models;

public readonly record struct DishModel
(
    int DishId,
    int MakeTimeMin,
    string DishRecipeUrl,
    string DishName,
    string MainCategory,
    string PreparationDifficulty,
    string IngredientsCategory,
    string IngredientsTags
);