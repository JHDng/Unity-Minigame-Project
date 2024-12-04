using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class ServingPoint : Box
{
    [Header("Settings")]
    [SerializeField] int rightDishPoints = 20;
    [SerializeField] int wrongDishPoints = 15;
    [Header("References")]
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

    public override IEnumerator ExtractIngredient(CharacterInteractionScript interScript, CharacterMovementScript movScript) //Put ingredient down
    {
        if(interScript.heldIngredient.ingredients[0].ingredientState > 0)
        {
            movScript.isEngaged = true;

            if(!exceptionIngredients.Contains(interScript.heldIngredient.ingredients[0]))
            {
                UpdateDish(interScript);
            }

            movScript.isEngaged = false;
        }
        yield return null;
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

        dishOnTable.ingredients[i] = player.heldIngredient.ingredients[0];

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

        if(rightIndex != -1)
        {
            SpriteRenderer spriteRenderer = displayDishPoint.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = dishes[rightIndex].sprite;
        }

        player.EmptyHands();

        CheckIfRightOrder();
    }

    void CheckIfRightOrder()
    {
        GameObject completedOrder;
        bool[] flags = {false, false, false};
        bool outerFlag = true;
        for(int i = 0; i < 3 && outerFlag; i++)
        {
            if(sceneManager.ordersPositionObjects[i].transform.childCount > 0)
            {
                IngredientHolder dishOfOrder = sceneManager.ordersPositionObjects[i].GetComponentInChildren<Order>().Dish;
                int j = 0;
                while(j < dishOnTable.ingredients.Length && (!flags[0] || !flags[1] || !flags[2]))
                {
                    bool found = false;
                    int z = 0;

                    while(!found && z < dishOnTable.ingredients.Length)
                    {
                        if(dishOnTable.ingredients[j].Equals(dishOfOrder.ingredients[z]) && flags[z] == false)
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

                    sceneManager.AddOrders();
                    totalScore += rightDishPoints;
                    outerFlag = false;

                    FreePlate();
                }
                else if(i == 2)
                {
                    totalScore -= wrongDishPoints;

                    FreePlate();
                }
                scoreText.text = "" + totalScore;
            }
        }
    }

    void FreePlate()
    {
        dishOnTable.sprite = null;
        dishOnTableSprite.sprite = emptyPlate;
        for(int k = 0; k < dishOnTable.ingredients.Length; k++)
        {
            dishOnTable.ingredients[k] = nullIngredient;
        }
    }
}
