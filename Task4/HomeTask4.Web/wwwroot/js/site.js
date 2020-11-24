// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
    var counterIngredient = 1;
    function AddIngredient() {
        $("#ingredientAddButton").click(function () {
            var valueIngredients = $("#ingredient").html();
            $("#ingredient").html(valueIngredients + "<label>Add ingredient " + counterIngredient + "</label><br /><input type='text' name='ingredients'/><br /><label>Add count for ingredient " + counterIngredient + "</label><br /><input type='text' name='countIngredients'/><br/ >");
            counterIngredient++;
        });
    }
    var counterSteps = 1;
    function AddSteps() {
            $("#stepsAddButton").click(function () {
                var valueSteps = $("#step").html();
                $("#step").html(valueSteps + "<label>Add steps " + counterSteps + "</label><br /><input type='text' name='stepsHowCooking'/><br />");
                counterSteps++;
            });
    }