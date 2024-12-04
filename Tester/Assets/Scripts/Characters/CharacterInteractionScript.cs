using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteractionScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject holdingPoint;
    [SerializeField] GameObject sliderCanvas;
    [SerializeField] Animator animator;
    [SerializeField] Slider slider;
    [HideInInspector] public IngredientHolder heldIngredient;
    [HideInInspector] public Ingredient nullIngredient;

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

    public IEnumerator StartTimer(float timeToCompleteTask)
    {
        sliderCanvas.SetActive(true);
        float timer = 0;
        
        while(timer < timeToCompleteTask)
        {
            timer += Time.deltaTime;
            slider.value = timer / timeToCompleteTask;
            yield return new WaitForSeconds(0.01f);
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

    public void StartStoveAnimation()
    {
        animator.SetBool("isStoving", true);
    }

    public void StopStoveAnimation()
    {
        animator.SetBool("isStoving", false);
    }
}
