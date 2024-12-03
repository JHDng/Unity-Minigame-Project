using UnityEngine;
public class Order : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int time;
    [SerializeField] int outOfTimePoints = 10;
    [Header("References")]
    [SerializeField] IngredientHolder[] Dishes;
    [SerializeField] UnityEngine.UI.Image dishSprite;
    [SerializeField] UnityEngine.UI.Image[] ingredientsSprite;
    [SerializeField] UnityEngine.UI.Image[] prepModeSprite;
    [SerializeField] Sprite[] prepSprites;
    [SerializeField] GameObject timeBar;
    [SerializeField] Sprite nullSprite;
    ServingPoint servingPointScript;
    public IngredientHolder Dish;

    void Awake()
    {
        int index = Random.Range(0, Dishes.Length);
        Dish = Dishes[index];

        servingPointScript = GameObject.Find("ServingPoint").transform.GetChild(0).GetComponent<ServingPoint>();
    }
    void Start()
    {
        dishSprite.sprite = Dish.sprite;

        for (int i = 0; i < 3; i++)
        {
            if(Dish.ingredients[i].sprite == null)
            {
                ingredientsSprite[i].sprite = nullSprite;
            }
            else
            {
                ingredientsSprite[i].sprite = Dish.ingredients[i].miniSprite;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            switch(Dish.ingredients[i].ingredientState)
            {
                case 1:
                    prepModeSprite[i].sprite = nullSprite;
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
        AnimateBar();
    }
    
    private void AnimateBar()
    {
        LeanTween.scaleX(timeBar, 0, time).setOnComplete(DestroyOrder);
    }

    private void DestroyOrder()
    {
        servingPointScript.totalScore -= outOfTimePoints;
        servingPointScript.scoreText.text = "" + servingPointScript.totalScore;
        Destroy(gameObject);
    }
}