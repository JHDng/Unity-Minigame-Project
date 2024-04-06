using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Player1Movement player1MovementScript;
    [SerializeField] Player2Movement player2MovementScript;
    [SerializeField] Player1Interaction player1IntScript;
    [SerializeField] Player2Interaction player2IntScript;
    private Queue<int> queue = new Queue<int>();
    private GameObject hitGameObject;
    private static int player1Int = 1;
    private static int player2Int = 2;
    private bool alternatorBool = true;
    void Update()
    {
        CheckTouchOnChefs();
        
        CheckQueue();

        OnQueueFilling();
    }

    void CheckTouchOnChefs()
    {
        //Debug.Log(queue.Count);
        if(!player1MovementScript.isSelected && !player2MovementScript.isMoving)
        {
            bool temp = player1MovementScript.FindChef(alternatorBool);
            if(temp) alternatorBool = !alternatorBool;
        }
        if(!player2MovementScript.isSelected && !player2MovementScript.isMoving)
        {
            bool temp = player2MovementScript.FindChef(alternatorBool);
            if(temp) alternatorBool = !alternatorBool;
        }
    }
    void CheckQueue()
    {
        if(player1MovementScript.isSelected && !queue.Contains(player1Int))
        {
            queue.Enqueue(player1Int);
        }
        if(player2MovementScript.isSelected && !queue.Contains(player2Int))
        {
            queue.Enqueue(player2Int);
        }
    }

    void OnQueueFilling()
    {
        if(queue.Count != 0)
        {
            if(FindTouch() && hitGameObject.layer == 7)
            {
                Box hitObjectScript = hitGameObject.GetComponent<Box>();
                int dequeueValue = queue.Dequeue();
                if(dequeueValue == 1) // ho cliccato prima il giocatore 1
                {
                    bool can1GoToFurniture = player1IntScript.heldIngredient.ingredientState == hitObjectScript.acceptableChefState? true: false;

                    if(can1GoToFurniture)
                    {
                        player1MovementScript.Deselected();
                        hitObjectScript.ComeHere(true);
                    }
                }
                else if(dequeueValue == 2) // ho cliccato prima il giocatore 2
                {
                    bool can2GoToFurniture = player2IntScript.heldIngredient.ingredientState == hitObjectScript.acceptableChefState? true: false;

                    if(can2GoToFurniture)
                    {
                        player2MovementScript.Deselected();
                        hitObjectScript.ComeHere(false);
                    }
                }
            }
        }
    }
    

    bool FindTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if(hit)
                {
                    hitGameObject = hit.collider.gameObject;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
