using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingIntoWaterThing : MonoBehaviour {
    private Box box;

    private void Start()
    {
        box =  Box.FindMy(BoxType.stand, BoxGender.pepee, gameObject);
        box.onEnter.AddListener(EnterWater);
        box.onExit.AddListener(ExitWater);
    }

    private void ExitWater(GameObject arg0)
    {
        var s = arg0.GetComponent<StateMachine>();
        if (s)
        {
            s.SetFalse(Terms.wetVar);
        }
    }

    private void EnterWater(GameObject arg0)
    {
        var s = arg0.GetComponent<StateMachine>();
        if (s)
        {
            s.SetTrue(Terms.wetVar);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " entering water");
        if (collision.GetComponent<Standing>() == null) { return; }
        if (collision.tag == "standingBox") ;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Standing>()) { return; }
        
    }
}
