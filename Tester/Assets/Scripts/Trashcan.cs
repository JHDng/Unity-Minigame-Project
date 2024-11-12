using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Trashcan : Box
{
    [SerializeField] Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    override public void ExtractIngredient(CharacterInteractionScript interScript, CharacterMovementScript movScript)
    {
        if(interScript.heldIngredient.ingredients[0] != interScript.nullIngredient)  animator.SetTrigger("Clicked");
        movScript.isEngaged = true;
        interScript.EmptyHands();
        movScript.isEngaged = false;
}
}
