using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    [SerializeField] private GameObject pointerRef;
    [SerializeField] private Animator pointerAnimator;
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
    private bool rotateClockwise = true;
    public bool FindChef()
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

    public IEnumerator RotateBackAndForth()
    {
        while (true)
        {
            if (rotateClockwise)
            {
                while (transform.rotation.eulerAngles.z < maxRotationAngle || transform.rotation.eulerAngles.z > 180)
                {
                    transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            else
            {
                while (transform.rotation.eulerAngles.z > 360 - maxRotationAngle || transform.rotation.eulerAngles.z < 180)
                {
                    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                    yield return null;
                }
            }
            rotateClockwise = !rotateClockwise;
        }
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
