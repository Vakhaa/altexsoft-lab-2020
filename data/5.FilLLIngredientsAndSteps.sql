--InsertIngredientsInRecipe: Recipe Id, Ingredient Id, CountIngredient
--InsertStepsInRecipe: Recipe Id, Description Steps

EXEC InsertIngredientsInRecipe 1, 4, N'1 шт.';
EXEC InsertIngredientsInRecipe 1, 10, N'1 л.';

EXEC InsertStepsInRecipe 1, N'Попросить маму, пускай варит'
EXEC InsertStepsInRecipe 1, N'Кушать'

EXEC InsertIngredientsInRecipe 2, 1, N'по вкусу';
EXEC InsertIngredientsInRecipe 2, 2, N'1 л.';

EXEC InsertStepsInRecipe 2, N'Приехать в Польшу'
EXEC InsertStepsInRecipe 2, N'Попробывать'

EXEC InsertIngredientsInRecipe 3, 11, N'300 г';
EXEC InsertIngredientsInRecipe 3, 12, N'300 мг';

EXEC InsertStepsInRecipe 3, N'Варить гречу, в молоке'
EXEC InsertStepsInRecipe 3, N'Кушать'

EXEC InsertIngredientsInRecipe 4, 8, N'Много, наверное';
EXEC InsertIngredientsInRecipe 4, 5, N'5 шт.';

EXEC InsertStepsInRecipe 4, N'Угостить соседа грузина, шанс дропа алкоголя крайне велик'
EXEC InsertStepsInRecipe 4, N'Выпивать'

EXEC InsertIngredientsInRecipe 5, 13, N'Много, наверное';
EXEC InsertIngredientsInRecipe 5, 14, N'3 ст. ложки';

EXEC InsertStepsInRecipe 5, N'Замесить тесто'
EXEC InsertStepsInRecipe 5, N'Поставить в духовку'
EXEC InsertStepsInRecipe 5, N'Достать и кушоть'

EXEC InsertIngredientsInRecipe 6, 13, N'Много, наверное';
EXEC InsertIngredientsInRecipe 6, 14, N'5 ст. ложки';

EXEC InsertStepsInRecipe 6, N'Замесить тесто'
EXEC InsertStepsInRecipe 6, N'Сделать основу'
EXEC InsertStepsInRecipe 6, N'Сделать крем'
EXEC InsertStepsInRecipe 6, N'Сделать чай и кушоть'

EXEC InsertIngredientsInRecipe 7, 13, N'Много, наверное';
EXEC InsertIngredientsInRecipe 7, 14, N'2 ст. ложки';

EXEC InsertStepsInRecipe 7, N'Замесить тесто'
EXEC InsertStepsInRecipe 7, N'Налепить печенек'
EXEC InsertStepsInRecipe 7, N'Выпечь, дальше поджарить на масле и посыпать пудрой'
EXEC InsertStepsInRecipe 7, N'Сделать чай и кушоть'