using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollowBrain : AIController
{

    private float searchForTarget;


    new private void Start()
    {
        base.Start();
        searchForTarget = 0;
    }




    private void Update()
    {
        if (target != null)
        {
            mover.SetDirection(target.position - transform.position);
        }

        if (searchForTarget > 1)
        {
            searchForTarget = 0;
			GetNewTarget();
        }
        searchForTarget += Time.deltaTime;
    }

    public void GetNewTarget()
    {
        float minDist = Mathf.Infinity;
		Transform tempTarget = null;
		if (target != null && !ValidTarget (target.gameObject)) {
            UnAssignTarget();
		}
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            print("checking this guy " + g.name);
            if (Vector2.Distance(g.transform.position, transform.position) < minDist)
            {
				if (ValidTarget(g)) {
                    minDist = Vector2.Distance(g.transform.position, transform.position);
                    tempTarget = g.transform;
                    print("i want to kill " + g.name);
                }
                
            }

        }
		if (target != tempTarget) {
			AssignNewTarget (tempTarget);
		}
    }

	protected override void AssignNewTarget(Transform tempTarget) {
        PlayCry();
        base.AssignNewTarget(tempTarget);
    }


    protected virtual bool ValidTarget(GameObject targ)
    {
        return true;
    }

	protected virtual void PlayCry () {
	}
}
