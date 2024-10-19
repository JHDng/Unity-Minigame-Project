using UnityEngine;

public class CuttingBoard : Box
{
    [Header("Ingredient")]
    [SerializeField] Ingredient[] ingredientsStored;
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
            Animator animator = interScript.gameObject.GetComponent<Animator>();
            movScript.isEngaged = true;
            animator.SetBool("isCutting", true);
            SpriteRenderer spriteRenderer = interScript.holdingPoint.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = null;
            await interScript.StartTimer(timeToPrepare);
            animator.SetBool("isCutting", false);
            interScript.TakeSomething(ingredientsStored[interScript.heldIngredient.ingredients[0].ingredientIndex]);
            movScript.isEngaged = false;

        }
    }
}
