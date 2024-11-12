using UnityEngine;

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
    
    override public async void ExtractIngredient(CharacterInteractionScript interScript , CharacterMovementScript movScript)
    {
        if(interScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            animator.SetBool("engaged", true);
            interScript.StartStoveAnimation();
            interScript.holdingPoint.GetComponent<SpriteRenderer>().sprite = null;
            movScript.isEngaged = true;

            await interScript.StartTimer(timeToPrepare);
            interScript.TakeSomething(ingredientsStored[interScript.heldIngredient.ingredients[0].ingredientIndex]);
            
            animator.SetBool("engaged", false);
            interScript.StopStoveAnimation();
            movScript.isEngaged = false;
        }
    }
}
