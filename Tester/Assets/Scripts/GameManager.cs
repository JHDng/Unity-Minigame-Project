using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Character Management")]
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Player1Movement player1MovementScript;
    [SerializeField] Player2Movement player2MovementScript;
    [SerializeField] Player1Interaction player1IntScript;
    [SerializeField] Player2Interaction player2IntScript;
    [Header("UI Management")]
    [SerializeField] TextMeshProUGUI scoreText;
    private Queue<int> queue = new Queue<int>();
    private GameObject hitGameObject;
    private static int player1Int = 1;
    private static int player2Int = 2;
    void Update()
    {
        CheckTouchOnChefs();
        
        CheckQueue();

        OnQueueFilling();

        OrderLayer();
    }

    void CheckTouchOnChefs()
    {
        if(!player1MovementScript.isSelected && !player1MovementScript.isMoving && !player1MovementScript.isEngaged)
        {
            player1MovementScript.FindChef();
        }
        if(!player2MovementScript.isSelected && !player2MovementScript.isMoving && !player2MovementScript.isEngaged)
        {
            player2MovementScript.FindChef();
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

                StartCoroutine(HoldOccupation(hitObjectScript));

                int dequeueValue = queue.Dequeue();

                if(dequeueValue == 1) // ho cliccato prima il giocatore 1
                {
                    player1MovementScript.Deselected();
                    hitObjectScript.ComeHere(true);
                }
                else if(dequeueValue == 2) // ho cliccato prima il giocatore 2
                {
                    player2MovementScript.Deselected();
                    hitObjectScript.ComeHere(false);
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

    IEnumerator HoldOccupation(Box box)
    {
        if( player1MovementScript.givenPosition == (Vector2) transform.TransformDirection(box.extractionPoint.transform.position) ||
            player2MovementScript.givenPosition == (Vector2) transform.TransformDirection(box.extractionPoint.transform.position))
        {
            box.isOccupied = true;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(HoldOccupation(box));
        }
        else
        {
            box.isOccupied = false;
        }
    }

    private void OrderLayer()
    {
        if(player1.transform.position.y < player2.transform.position.y)
        {
            player1.GetComponent<SpriteRenderer>().sortingOrder = 7;
            player1IntScript.OrderHoldingPoint(true);
        }
        else
        {
            player1.GetComponent<SpriteRenderer>().sortingOrder = 4;
            player1IntScript.OrderHoldingPoint(false);
        }
    }

    //UI MANAGEMENT


}
