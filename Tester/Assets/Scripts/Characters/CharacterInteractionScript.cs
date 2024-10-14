using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteractionScript : MonoBehaviour
{
    [SerializeField] GameObject sliderCanvas;
    [SerializeField] public GameObject holdingPoint;
    [SerializeField] Animator animator;
    [SerializeField] Slider slider;
    public IngredientHolder heldIngredient;
    public Ingredient nullIngredient;

    public void TakeSomething(Ingredient takenIngredient)
    {
        SpriteRenderer spriteRenderer = holdingPoint.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = takenIngredient.sprite;
        
        animator.SetBool("isHolding", true);

        heldIngredient.ingredients[0] = takenIngredient;
    }

    public void EmptyHands()
    {
        SpriteRenderer spriteRenderer = holdingPoint.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;

        heldIngredient.ingredients[0] = nullIngredient;

        animator.SetBool("isHolding", false);
    }

    public async Task StartTimer(float timeToCompleteTask)
    {
        sliderCanvas.SetActive(true);
        float timer = 0;
        
        while(timer < timeToCompleteTask)
        {
            timer += Time.deltaTime;
            slider.value = timer / timeToCompleteTask;
            await Task.Delay(1);
        }

        sliderCanvas.SetActive(false);
    }

    public void OrderHoldingPoint(bool yLower)
    {
        SpriteRenderer temp = holdingPoint.GetComponent<SpriteRenderer>();
        if(yLower)
        {
            temp.sortingOrder = 8;
        }
        else
        {
            temp.sortingOrder = 5;
        }
    }
}
