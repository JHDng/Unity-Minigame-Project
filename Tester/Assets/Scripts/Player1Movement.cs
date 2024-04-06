using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    [SerializeField] private GameObject pointerRef;
    [SerializeField] private Animator pointerAnimator;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    public Vector2 givenPosition = new Vector2();
    public Box destinationFurniture;
    public bool isSelected = false;
    public bool isMoving = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(isMoving)
        {
            MovePlayer(givenPosition);
        }
    }
    public bool FindChef(bool iAmFirst)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    animator.SetTrigger("isSelected");
                    Selected(iAmFirst);
                    return true;
                }
            }
        }
        return false;
    }

    public void MovePlayer(Vector2 givenPosition)
    {
        Deselected();
        transform.position = Vector3.MoveTowards(transform.position, givenPosition, moveSpeed);
        animator.SetBool("isRunning", true);
        if(new Vector2(transform.position.x, transform.position.y) == givenPosition)
        {
            isMoving = false;
            animator.SetBool("isRunning", false);

            destinationFurniture.ExtractIngredient(true); // different when paste
        }
    }

    private void Selected(bool iAmFirst)
    {
        isSelected = true;
        pointerRef.SetActive(true);
        if(iAmFirst)
        {
            pointerAnimator.SetBool("firstTouched", true);
        }
        else
        {
            pointerAnimator.SetBool("firstTouched", false);
        }
    }

    public void Deselected()
    {
        isSelected = false;
        pointerRef.SetActive(false);
    }
}
