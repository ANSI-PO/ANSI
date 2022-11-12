namespace Domain.Models;

public readonly record struct DishModel
(
    int DishId,
    int MakeTimeMin,
    Uri? DishRecipeUrl,
    string DishName,
    string MainCategory,
    string PreparationDifficulty,
    string IngredientsCategory,
    string IngredientsTags
);