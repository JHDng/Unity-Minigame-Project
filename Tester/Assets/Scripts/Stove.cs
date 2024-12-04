using UnityEngine;
using System.Collections;

public class Stove : Box
{
    [Header("Ingredient")]
    [SerializeField] Ingredient[] ingredientsStored;
    [SerializeField] Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public override IEnumerator ExtractIngredient(CharacterInteractionScript interScript , CharacterMovementScript movScript)
    {
        if(interScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            animator.SetBool("engaged", true);
            interScript.StartStoveAnimation();
            interScript.holdingPoint.GetComponent<SpriteRenderer>().sprite = null;
            movScript.isEngaged = true;

            yield return StartCoroutine(interScript.StartTimer(timeToPrepare));
            interScript.TakeSomething(ingredientsStored[interScript.heldIngredient.ingredients[0].ingredientIndex]);
            
            animator.SetBool("engaged", false);
            interScript.StopStoveAnimation();
            movScript.isEngaged = false;
        }
    }
}
