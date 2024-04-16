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
        if(iAmOne && player1IntScript.heldIngredient.ingredientState == acceptableChefState)
        {
            player1MovScript.isEngaged = true;
            await player1IntScript.StartTimer(timeToPrepare);
            player1IntScript.TakeSomething(ingredientsStored[player1IntScript.heldIngredient.ingredientIndex]);
            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne && player2IntScript.heldIngredient.ingredientState == acceptableChefState)
        {
            player2MovScript.isEngaged = true;
            await player2IntScript.StartTimer(timeToPrepare);
            player2IntScript.TakeSomething(ingredientsStored[player2IntScript.heldIngredient.ingredientIndex]);
            player2MovScript.isEngaged = false;
        }
    }
}
