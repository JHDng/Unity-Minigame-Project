using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    [SerializeField] private GameObject pointerRef;
    [SerializeField] private Animator pointerAnimator;
    [SerializeField] private LayerMask characterLayer;
    [SerializeField] public Animator animator;
    [SerializeField] public float moveSpeed;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float maxRotationAngle = 2;
    public Vector2 givenPosition = new Vector2();
    public Box destinationFurniture;
    public bool isSelected = false;
    public bool isMoving = false;
    public bool isEngaged = false;
    public bool executeRotateAnimation = false;

    void Start()
    {
        
    }
    public bool FindChef()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
                Collider2D hit = Physics2D.OverlapPoint(touchPosition, characterLayer);

                if (hit != null && hit.gameObject == gameObject)
                {
                    animator.SetTrigger("isSelected");
                    Selected();
                    return true;
                }
            }
        }
        return false;
    }

    virtual public void MovePlayer(Vector2 givenPosition)
    {
        Deselected();
        transform.position = Vector3.MoveTowards(transform.position, givenPosition, moveSpeed * Time.deltaTime);
        animator.SetBool("isRunning", true);
        if(new Vector2(transform.position.x, transform.position.y) == givenPosition)
        {
            isMoving = false;
            animator.SetBool("isRunning", false);

            destinationFurniture.ExtractIngredient(true); // different when paste
        }
    }

    public void RotateBackAndForth()
    {
        float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    private void Selected()
    {
        isSelected = true;
        pointerRef.SetActive(true);
    }

    public void Deselected()
    {
        isSelected = false;
        pointerRef.SetActive(false);
    }
}
