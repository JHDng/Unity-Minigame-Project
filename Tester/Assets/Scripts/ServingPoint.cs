using System.Linq;
using TMPro;
using UnityEngine;

public class ServingPoint : Box
{
    [SerializeField] IngredientHolder[] dishes;
    [SerializeField] Ingredient[] exceptionIngredients;
    [SerializeField] GameObject displayDishPoint;
    [SerializeField] Ingredient nullIngredient;
    public IngredientHolder dishOnTable;
    void Start()
    {
        for(int i = 0; i < dishOnTable.ingredients.Length; i++)
        {
            dishOnTable.ingredients[i] = nullIngredient;
        }
    }

    void Update()
    {
        
    }

    public override void ExtractIngredient(bool iAmOne) //Put ingredient down
    {
        if(iAmOne && player1IntScript.heldIngredient.ingredients[0].ingredientState > 0)
        {
            player1MovScript.isEngaged = true;

            if(!exceptionIngredients.Contains(player1IntScript.heldIngredient.ingredients[0]))
            {
                UpdateDish(player1IntScript);
            }

            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne && player2IntScript.heldIngredient.ingredients[0].ingredientState > 0)
        {
            player2MovScript.isEngaged = true;

            if(!exceptionIngredients.Contains(player2IntScript.heldIngredient.ingredients[0]))
            {
                UpdateDish(player2IntScript);
            }

            player2MovScript.isEngaged = false;
        }
    }

    private void UpdateDish(CharacterInteractionScript player)
    {
        if(dishOnTable.ingredients[0] == nullIngredient)
        {
            UpdateSimpleDish(player);
        }
        else
        {
            UpdateComplexDish(player);
        }
    }


    private void UpdateSimpleDish(CharacterInteractionScript player)
    {
        Ingredient tempIngredient = player.heldIngredient.ingredients[0];

        dishOnTable.ingredients[0] = tempIngredient;

        SpriteRenderer spriteRenderer = displayDishPoint.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = tempIngredient.platedIngredient;

        player.EmptyHands();
    }

    private void UpdateComplexDish(CharacterInteractionScript player)
    {
        int i = 0;
        int rightIndex = -1;
        while(dishOnTable.ingredients[i] != nullIngredient && i < dishOnTable.ingredients.Length)
        {
            i++;
        }
        Ingredient tempIngredient = player.heldIngredient.ingredients[0];

        dishOnTable.ingredients[i] = tempIngredient;

        for(int k = 0; k < dishes.Length && rightIndex == -1; k++)
        {
            int testPassed = 0;
            for(int j = 0; j < dishOnTable.ingredients.Length && rightIndex == -1; j++)
            {
                for(int z = 0; z < dishes[k].ingredients.Length && rightIndex == -1; z++)
                {
                    if(dishOnTable.ingredients[j] == dishes[k].ingredients[z])
                    {
                        testPassed++;
                    }
                    if(testPassed == dishOnTable.ingredients.Length)
                    {
                        rightIndex = k;
                    }
                }
            }
        }

        SpriteRenderer spriteRenderer = displayDishPoint.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dishes[rightIndex].sprite;

        player.EmptyHands();
    }
}
