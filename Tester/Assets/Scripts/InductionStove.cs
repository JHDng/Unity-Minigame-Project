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

    override public async void ExtractIngredient(CharacterInteractionScript interScript, CharacterMovementScript movScript)
    {
        if(interScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            movScript.isEngaged = true;
            await interScript.StartTimer(timeToPrepare);
            interScript.TakeSomething(ingredientsStored[interScript.heldIngredient.ingredients[0].ingredientIndex]);
            movScript.isEngaged = false;
        }
    }
}
