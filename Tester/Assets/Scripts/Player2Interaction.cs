using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Player2Interaction : MonoBehaviour
{
    [SerializeField] GameObject sliderCanvas;
    [SerializeField] GameObject holdingPoint;
    [SerializeField] Animator animator;
    [SerializeField] Slider slider;
    public Ingredient heldIngredient;
    
    void Start()
    {
        heldIngredient.sprite = null;
        heldIngredient.ingredientIndex = -1;
        heldIngredient.ingredientState = -1;
        heldIngredient.ingredientFinalState = 100;
    }

    public void TakeSomething(Ingredient takenIngredient)
    {
        if(heldIngredient.ingredientState == -1)
        {
            SpriteRenderer spriteRenderer = holdingPoint.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = takenIngredient.sprite;

            animator.SetBool("isHolding", true);

            heldIngredient.ingredientIndex = takenIngredient.ingredientIndex;
            heldIngredient.ingredientState = takenIngredient.ingredientState;
            heldIngredient.ingredientFinalState = takenIngredient.ingredientFinalState;
        }
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
}
