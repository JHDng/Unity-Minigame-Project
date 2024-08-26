using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InductionStove : Box
{
    [Header("Ingredient")]
    [SerializeField] Ingredient[] ingredientsStored;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public async void ExtractIngredient(bool iAmOne)
    {
        if(iAmOne && player1IntScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            player1MovScript.isEngaged = true;
            await player1IntScript.StartTimer(timeToPrepare);
            player1IntScript.TakeSomething(ingredientsStored[player1IntScript.heldIngredient.ingredients[0].ingredientIndex]);
            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne && player2IntScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            player2MovScript.isEngaged = true;
            await player2IntScript.StartTimer(timeToPrepare);
            player2IntScript.TakeSomething(ingredientsStored[player2IntScript.heldIngredient.ingredients[0].ingredientIndex]);
            player2MovScript.isEngaged = false;
        }
    }
}
