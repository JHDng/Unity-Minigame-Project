using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Ingredient")]
    [SerializeField] Ingredient ingredientStored;
    [Header("References")]
    [SerializeField] Player1Interaction player1IntScript;
    [SerializeField] Player1Movement player1MovScript;
    [SerializeField] Player2Interaction player2IntScript;
    [SerializeField] Player2Movement player2MovScript;
    [SerializeField] Transform extractionPoint;
    [Header("Values")]
    [SerializeField] float timeToPrepare = 0.1f; //to override
    public int acceptableChefState = -1; // to override
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ComeHere(bool isPlayer1)
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

    public async void ExtractIngredient(bool iAmOne)
    {
        if(iAmOne)
        {
            await player1IntScript.StartTimer(timeToPrepare);
            player1IntScript.TakeSomething(ingredientStored);
        }
        else if(!iAmOne)
        {
            await player2IntScript.StartTimer(timeToPrepare);
            player2IntScript.TakeSomething(ingredientStored);
        }
    }
}
