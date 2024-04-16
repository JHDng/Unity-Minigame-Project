using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Ingredient")]
    [SerializeField] Ingredient ingredientStored;
    [Header("References")]
    [SerializeField] public Player1Interaction player1IntScript;
    [SerializeField] public Player1Movement player1MovScript;
    [SerializeField] public Player2Interaction player2IntScript;
    [SerializeField] public Player2Movement player2MovScript;
    [SerializeField] public Transform extractionPoint;
    [Header("Values")]
    [SerializeField] public float timeToPrepare = 0.1f; //to override
    public int acceptableChefState = -1; // to override
    public bool isOccupied = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ComeHere(bool isPlayer1)
    {
        if(!isOccupied)
        {
            if(isPlayer1)
            {
                player1MovScript.givenPosition = extractionPoint.position;
                player1MovScript.isMoving = true;
                player1MovScript.destinationFurniture = this;
            }
            else if(!isPlayer1)
            {
                player2MovScript.givenPosition = extractionPoint.position;
                player2MovScript.isMoving = true;
                player2MovScript.destinationFurniture = this;
            }
        }
    }

    virtual public async void ExtractIngredient(bool iAmOne)
    {
        if(iAmOne && player1IntScript.heldIngredient.ingredientState == acceptableChefState)
        {
            player1MovScript.isEngaged = true;
            await player1IntScript.StartTimer(timeToPrepare);
            player1IntScript.TakeSomething(ingredientStored);
            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne && player2IntScript.heldIngredient.ingredientState == acceptableChefState)
        {
            player2MovScript.isEngaged = true;
            await player2IntScript.StartTimer(timeToPrepare);
            player2IntScript.TakeSomething(ingredientStored);
            player2MovScript.isEngaged = false;
        }
    }
}
