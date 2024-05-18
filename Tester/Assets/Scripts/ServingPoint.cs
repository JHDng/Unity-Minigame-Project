using System.Linq;
using TMPro;
using UnityEngine;

public class ServingPoint : Box
{
    [SerializeField] IngredientHolder[] dishes;
    [SerializeField] GameObject displayDishPoint;
    public IngredientHolder dishOnTable;
    void Start()
    {
        for(int i = 0; i < dishOnTable.ingredients.Length; i++)
        {
            dishOnTable.ingredients[i] = null;
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

            if(dishOnTable.ingredients[0] == null)
            {
                UpdateSimpleDish(player1IntScript);
            }
            else
            {
                UpdateComplexDish(player1IntScript);
            }

            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne && player2IntScript.heldIngredient.ingredients[0].ingredientState > 0)
        {
            player2MovScript.isEngaged = true;

            if(dishOnTable.ingredients[0] == null)
            {
                UpdateSimpleDish(player2IntScript);
            }
            else
            {
                UpdateComplexDish(player2IntScript);
            }

            player2MovScript.isEngaged = false;
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
        int i = 0, j = 0;
        while(dishOnTable.ingredients[i] != null)
        {
            i++;
        }
        Ingredient tempIngredient = player.heldIngredient.ingredients[0];

        dishOnTable.ingredients[i] = tempIngredient;

        //fare affinche tutti gli ingredienti del dishontable siano in uno dei dishes (le varie combinazioni)
        while(dishes[j] != dishOnTable)
        {
            j++;
        }

        SpriteRenderer spriteRenderer = displayDishPoint.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dishes[j].sprite;

        player.EmptyHands();
    }
}
