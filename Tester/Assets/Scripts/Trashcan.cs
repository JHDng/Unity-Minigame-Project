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

    override public void ExtractIngredient(bool iAmOne)
    {
        if(iAmOne)
        {
            if(player1IntScript.heldIngredient.ingredients[0] != player1IntScript.nullIngredient)  animator.SetTrigger("Clicked");
            player1MovScript.isEngaged = true;
            player1IntScript.EmptyHands();
            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne)
        {
            if(player2IntScript.heldIngredient.ingredients[0] != player2IntScript.nullIngredient)  animator.SetTrigger("Clicked");
            player2MovScript.isEngaged = true;
            player2IntScript.EmptyHands();
            player2MovScript.isEngaged = false;
        }
    }
}
