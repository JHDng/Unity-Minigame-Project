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

    override public async void ExtractIngredient(bool iAmOne)
    {
        if(iAmOne && player1IntScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            Animator animator = player1IntScript.gameObject.GetComponent<Animator>();
            player1MovScript.isEngaged = true;
            animator.SetBool("isCutting", true);
            SpriteRenderer spriteRenderer = player1IntScript.holdingPoint.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = null;
            await player1IntScript.StartTimer(timeToPrepare);
            animator.SetBool("isCutting", false);
            player1IntScript.TakeSomething(ingredientsStored[player1IntScript.heldIngredient.ingredients[0].ingredientIndex]);
            player1MovScript.isEngaged = false;

        }
        else if(!iAmOne && player2IntScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            Animator animator = player2IntScript.gameObject.GetComponent<Animator>();
            player2MovScript.isEngaged = true;
            animator.SetBool("isCutting", true);
            SpriteRenderer spriteRenderer = player2IntScript.holdingPoint.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = null;
            await player2IntScript.StartTimer(timeToPrepare);
            animator.SetBool("isCutting", false);
            player2IntScript.TakeSomething(ingredientsStored[player2IntScript.heldIngredient.ingredients[0].ingredientIndex]);
            player2MovScript.isEngaged = false;
        }
    }
}
