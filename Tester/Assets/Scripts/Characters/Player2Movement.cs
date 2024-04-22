using UnityEngine;

public class Player2Movement : CharacterMovementScript
{
    void Start()
    {
        
    }

    void Update()
    {
        if(isMoving)
        {
            MovePlayer(givenPosition);
        }
        else if(!isMoving)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    override public void MovePlayer(Vector2 givenPosition)
    {
        Deselected();
        transform.position = Vector3.MoveTowards(transform.position, givenPosition, moveSpeed * Time.deltaTime);
        animator.SetBool("isRunning", true);
        if(new Vector2(transform.position.x, transform.position.y) == givenPosition)
        {
            isMoving = false;
            animator.SetBool("isRunning", false);

            destinationFurniture.ExtractIngredient(false); // different when paste
        }
    }
}
