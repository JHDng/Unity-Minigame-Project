using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneInsideManager : MonoBehaviour
{
    [Header("Character Management")]
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Player1Movement player1MovementScript;
    [SerializeField] Player2Movement player2MovementScript;
    [SerializeField] Player1Interaction player1IntScript;
    [SerializeField] Player2Interaction player2IntScript;
    [SerializeField] OrdersButton ordersButtonScript;
    [Header("UI Management")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] public GameObject[] ordersPositionObjects;
    [SerializeField] GameObject orderPrefab;
    [Header("Countdown Management")]
    [SerializeField] int countdownTime;
    [SerializeField] GameObject countdownTextObject;
    private Queue<CharacterMovementScript> queue = new Queue<CharacterMovementScript>();
    private GameObject hitGameObject;
    private bool timerON = true;
    
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if(!timerON)
        {
            CheckTouchOnChefs();

            CheckQueue();

            OnQueueFilling();

            OrderLayer();

            CheckForOrderBar();

            AddOrders();
        }
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
        if(player1MovementScript.isSelected && !queue.Contains(player1MovementScript))
        {
            queue.Enqueue(player1MovementScript);
        }
        if(player2MovementScript.isSelected && !queue.Contains(player2MovementScript))
        {
            queue.Enqueue(player2MovementScript);
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

                CharacterMovementScript dequeued = queue.Dequeue();
                dequeued.Deselected();
                hitObjectScript.ComeHere(dequeued);
            }
        }
    }
    
    bool FindTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

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

    private IEnumerator StartCountdown()
    {
        TextMeshProUGUI countdownText = countdownTextObject.GetComponent<TextMeshProUGUI>();

        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1);

            countdownTime--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(1);

        Destroy(countdownTextObject.transform.parent.gameObject);

        timerON = false;
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

    private void CheckForOrderBar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(ordersButtonScript.buttonSwitch == false)
            {
                ordersButtonScript.HideOrderBar();
            }
        }
    }

    public void AddOrders()
    {
        for(int i = 0; i < 3; i++)
        {
            if(ordersPositionObjects[i].transform.childCount == 0)
            {
                GameObject order = Instantiate(orderPrefab, Vector3.zero, Quaternion.identity);
                order.transform.SetParent(ordersPositionObjects[i].transform, false);
                order.transform.localScale = new Vector3(12, 12, 12);
            }
        }
    }


}
