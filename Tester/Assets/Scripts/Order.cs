using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] IngredientHolder[] Dishes;
    [SerializeField] SpriteRenderer dishSprite;
    [SerializeField] SpriteRenderer[] ingredientsSprite;
    [SerializeField] SpriteRenderer[] prepModeSprite;
    [SerializeField] Sprite[] prepSprites;
    public IngredientHolder Dish;

    void Awake()
    {
        int index = Random.Range(0, Dishes.Length);

        Dish = Dishes[index];
    }
    void Start()
    {

        dishSprite.sprite = Dish.sprite;

        for (int i = 0; i < 3; i++)
        {
            ingredientsSprite[i].sprite = Dish.ingredients[i].miniSprite;
        }

        for (int i = 0; i < 3; i++)
        {
            switch(Dish.ingredients[i].ingredientState)
            {
                case 1:
                    prepModeSprite[i].sprite = null;
                    break;
                case 2:
                    prepModeSprite[i].sprite = prepSprites[0];
                    break;
                case 3:
                    prepModeSprite[i].sprite = prepSprites[1];
                    break;
                case 4:
                    prepModeSprite[i].sprite = prepSprites[2];
                    break;
            }
        }
    }
}
