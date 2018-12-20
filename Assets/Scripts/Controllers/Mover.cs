using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public Vector2 direction;
    public Vector2 facing;
    public float speed;
    private WalkingState walkState;
    private StateMachine statemachine;
    private Animator anim;

    private void Start()
    {
        statemachine = GetComponent<StateMachine>();
        if (this.GetComponent<Animator>())
        {
            anim = this.GetComponent<Animator>();
        }
    }

    protected virtual void Update()
    {
        statemachine.SetFloat(Terms.walkingVar, direction.magnitude);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.001f * transform.position.y);
    }

    public void SetDirection(float x, float y)
    {
        direction.x = x;
        direction.y = y;
        direction.Normalize();
        SetFacing(x, y);
        SetAnimDirection();

    }

    private void SetAnimDirection()
    {
        if (anim)
        {
            if (facing == Vector2.left)
            {
                anim.SetInteger("Direction", 3);
                //Debug.Log ("dir 1");
            }
            else if (facing == Vector2.right)
            {
                anim.SetInteger("Direction", 1);
                //Debug.Log ("dir 3");
            }
            else if (facing == Vector2.up)
            {
                anim.SetInteger("Direction", 0);
                //Debug.Log ("dir 0");
            }
            else if (facing == Vector2.down)
            {
                anim.SetInteger("Direction", 2);
                //Debug.Log ("dir 2");
            }
        }
    }

    private void SetFacing(float x, float y)
    {
        if (-x > Mathf.Abs(y))
        {
            facing = Vector2.left;
        }
        else if (x > Mathf.Abs(y))
        {
            facing = Vector2.right;
        }
        else if (y > 0)
        {
            facing = Vector2.up;
        }
        else if (y < 0)
        {
            facing = Vector2.down;
        }
    }

    public void SetDirection(Vector2 v)
    {
        SetDirection(v.x, v.y);
    }


}


/*
 * 
 * if (anim) 
		{
			if( -x > Mathf.Abs(y))
			{
				anim.SetInteger ("Direction", 3);
				//Debug.Log ("dir 1");
			}
			else if( x > Mathf.Abs(y))
			{
				anim.SetInteger ("Direction", 1);
				//Debug.Log ("dir 3");
			}
			else if(y>0)
			{
				anim.SetInteger ("Direction", 0);
				//Debug.Log ("dir 0");
			}
			else if(y<0)
			{
				anim.SetInteger ("Direction", 2);
				//Debug.Log ("dir 2");
			}
		}
*/
