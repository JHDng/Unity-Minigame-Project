using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : Box
{
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
            player1MovScript.isEngaged = true;
            player1IntScript.EmptyHands();
            player1MovScript.isEngaged = false;
        }
        else if(!iAmOne)
        {
            player2MovScript.isEngaged = true;
            player2IntScript.EmptyHands();
            player2MovScript.isEngaged = false;
        }
    }
}
