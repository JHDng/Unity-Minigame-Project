using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Ingredient")]
    [SerializeField] Ingredient ingredientStored;
    [Header("References")]
    [SerializeField] public Transform extractionPoint;
    [Header("Values")]
    [SerializeField] protected float timeToPrepare = 0.1f; //to override
    public int acceptableChefState = -1; // to override
    public bool isOccupied = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ComeHere(CharacterMovementScript movScript)
    {
        if(!isOccupied)
        {
            movScript.givenPosition = extractionPoint.position;
            movScript.isMoving = true;
            movScript.destinationFurniture = this;
        }
    }

    virtual public async void ExtractIngredient(CharacterInteractionScript interScript, CharacterMovementScript movScript)
    {
        if(interScript.heldIngredient.ingredients[0].ingredientState == acceptableChefState)
        {
            movScript.isEngaged = true;
            await interScript.StartTimer(timeToPrepare);
            interScript.TakeSomething(ingredientStored);
            movScript.isEngaged = false;
        }
    }
}
