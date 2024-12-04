using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : Box
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

    public override IEnumerator ExtractIngredient(CharacterInteractionScript interScript , CharacterMovementScript movScript)
    {
        if(interScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            movScript.isEngaged = true;
            yield return StartCoroutine(interScript.StartTimer(timeToPrepare));
            interScript.TakeSomething(ingredientsStored[interScript.heldIngredient.ingredients[0].ingredientIndex]);
            movScript.isEngaged = false;
        }
    }
}
