using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingIntoWaterThing : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entering water");
        var s = collision.GetComponent<StateMachine>(); 
        if(s)
        {
            s.SetTrue(Terms.wetVar);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var s =collision.GetComponent<StateMachine>();
        if(s)
        {
            s.SetFalse(Terms.wetVar);
        }
    }
}
