using UnityEngine;
using System.Collections;

public class CuttingBoard : Box
{
    [Header("Ingredient")]
    [SerializeField] Ingredient[] ingredientsStored;
    [SerializeField] Animator cuttingBoardAnimator;
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
            Animator animator = interScript.gameObject.GetComponent<Animator>();
            SpriteRenderer spriteRenderer = interScript.holdingPoint.GetComponent<SpriteRenderer>();

            cuttingBoardAnimator.SetBool("engaged", true);
            animator.SetBool("isCutting", true);
            movScript.isEngaged = true;
            spriteRenderer.sprite = null;

            yield return interScript.StartTimer(timeToPrepare);

            animator.SetBool("isCutting", false);
            cuttingBoardAnimator.SetBool("engaged", false);
            movScript.isEngaged = false;

            interScript.TakeSomething(ingredientsStored[interScript.heldIngredient.ingredients[0].ingredientIndex]);
        }
    }
}
