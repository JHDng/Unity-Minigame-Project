using UnityEngine;

public class Player1Movement : CharacterMovementScript
{
    void Start()
    {
        
    }
    
    void Update()
    {
        if(isMoving)
        {
            if(!walkingSoundPlaying)
            {
                audioSource.Play();
                walkingSoundPlaying = true;
            }

            MovePlayer(givenPosition);
            RotateBackAndForth();
        }
        else if(!isMoving)
        {
            audioSource.Stop();
            walkingSoundPlaying = false;
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

            StartCoroutine(destinationFurniture.ExtractIngredient(characterInteractionScript , this));
        }
    }

    protected override void Selected(AudioClip selectionClip, GameObject pointerRef)
    {
        audioSource.PlayOneShot(selectionClip);
        isSelected = true;
        pointerRef.SetActive(true);
    }
}
