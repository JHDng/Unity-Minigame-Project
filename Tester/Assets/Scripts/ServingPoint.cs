using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class ServingPoint : Box
{
    [SerializeField] IngredientHolder[] dishes;
    [SerializeField] Ingredient[] exceptionIngredients;
    [SerializeField] GameObject displayDishPoint;
    [SerializeField] Ingredient nullIngredient;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] SceneInsideManager sceneManager;
    [SerializeField] SpriteRenderer dishOnTableSprite;
    [SerializeField] Sprite emptyPlate;
    public IngredientHolder dishOnTable;
    public int totalScore = 0;
    void Start()
    {
        for(int i = 0; i < dishOnTable.ingredients.Length; i++)
        {
            dishOnTable.ingredients[i] = nullIngredient;
        }
        scoreText.text = "0";
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

        CheckIfRightOrder(dishOnTable);
    }

    void CheckIfRightOrder(IngredientHolder dish)
    {
        GameObject completedOrder;
        bool[] flags = {false, false, false};
        bool flag0 = true;
        for(int i = 0; i < 3 && flag0; i++)
        {
            if(sceneManager.ordersPositionObjects[i].transform.childCount > 0)
            {
                IngredientHolder dishOfOrder = sceneManager.ordersPositionObjects[i].GetComponentInChildren<Order>().Dish;
                int j = 0;
                while(j < dish.ingredients.Length && (!flags[0] || !flags[1] || !flags[2]))
                {
                    bool found = false;
                    int z = 0;

                    while(!found && z < dish.ingredients.Length)
                    {
                        if(dish.ingredients[j] == dishOfOrder.ingredients[z] && flags[z] == false)
                        {
                            flags[z] = true;
                            found = true;
                        }
                        z++;
                    }
                    j++;
                }
                if(flags[0] && flags[1] && flags[2])
                {
                    completedOrder = sceneManager.ordersPositionObjects[i].transform.GetChild(0).gameObject;
                    Destroy(completedOrder);

                    dishOnTable.sprite = null;
                    dishOnTableSprite.sprite = emptyPlate;
                    for(int k = 0; k < dishOnTable.ingredients.Length; k++)
                    {
                        dishOnTable.ingredients[k] = nullIngredient;
                    }

                    sceneManager.AddOrders();
                    totalScore += 20;
                }
                else
                {
                    dishOnTable.sprite = null;
                    dishOnTableSprite.sprite = emptyPlate;
                    for(int k = 0; k < dishOnTable.ingredients.Length; k++)
                    {
                        dishOnTable.ingredients[k] = nullIngredient;
                    }

                    totalScore -= 40;
                }
                
                flag0 = false;
                scoreText.text = "" + totalScore;
            }
        }


    }
}
