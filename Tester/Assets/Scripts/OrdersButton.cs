using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersButton : MonoBehaviour
{
    [SerializeField] private GameObject orderBar;
    private float tweenTime = 0.75f;
    private float showValue = 4f;
    private float hideValue = 12f;
    public bool buttonSwitch = true;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void ShowOrderBar()
    {
        Vector2 vector = new Vector2(0, showValue);
        LeanTween.move(orderBar, vector, tweenTime).setEaseOutQuart();
        buttonSwitch = false;
        this.gameObject.SetActive(false);
    }
    public void HideOrderBar()
    {
        Vector2 vector = new Vector2(0, hideValue);
        LeanTween.move(orderBar, vector, tweenTime).setEaseOutQuart();
        buttonSwitch = true;
        this.gameObject.SetActive(true);
    }
}
