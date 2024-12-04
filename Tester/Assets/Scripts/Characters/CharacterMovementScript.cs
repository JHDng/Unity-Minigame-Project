using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    [Header("Specifics")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float maxRotationAngle = 2;
    [Header("Animator")]
    [SerializeField] protected Animator animator;
    [Header("References")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip selection;
    [SerializeField] GameObject pointerRef;
    [SerializeField] LayerMask characterLayer;
    [SerializeField] protected CharacterInteractionScript characterInteractionScript;
    [Header("Checks")]
    public bool isSelected = false;
    public bool isMoving = false;
    public bool isEngaged = false;
    [HideInInspector]   public Vector2 givenPosition = new Vector2();
    [HideInInspector]   public Box destinationFurniture;
    protected bool walkingSoundPlaying = false;

    void Start()
    {
        
    }
    public bool FindChef()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit && hit.collider.gameObject == gameObject)
            {
                animator.SetTrigger("isSelected");
                Selected(selection, pointerRef);
                return true;
            }
        }
        return false;
    }

    virtual public void MovePlayer(Vector2 givenPosition)
    { 
    }

    public void RotateBackAndForth()
    {
        float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    virtual protected void Selected(AudioClip selectionClip, GameObject pointerRef)
    {
    }

    public void Deselected()
    {
        isSelected = false;
        pointerRef.SetActive(false);
    }
}
