using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    public Transform target = null;
    protected Mover mover;


    protected void Start()
    {
        mover = GetComponent<Mover>();
    }


    protected virtual void AssignNewTarget(Transform tempTarget)
    {
        print("re assigning");
        target = tempTarget;
        if (target.GetComponent<DeadState>() != null)
        {
            target.GetComponent<DeadState>().onDeath.AddListener(UnAssignTarget);

        }
    }

    protected virtual void UnAssignTarget()
    {
        print("asdf");
        if (target.GetComponent<DeadState>() != null)
        {
            target.GetComponent<DeadState>().onDeath.RemoveListener(UnAssignTarget);

        }
        target = null;
    }
    
}
