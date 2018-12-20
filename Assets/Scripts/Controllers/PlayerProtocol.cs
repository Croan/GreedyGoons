using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProtocol: MonoBehaviour
{
    private Puncher puncher;
    private Mover mover;
	private User user;

    public int playerNum;

    private void Start()
    {
        puncher = GetComponent<Puncher>();
        mover = GetComponent<Mover>();
		user = GetComponent<User>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("punch" + playerNum))
        {
            puncher.Punch();
        }

		if (Input.GetButtonDown("use" + playerNum))
        {
            user.Use();
        }
		if (Input.GetButtonUp("use" + playerNum))
		{
			user.StartUse();
		}
		mover.SetDirection (Input.GetAxisRaw ("Horizontal" + playerNum), Input.GetAxisRaw("Vertical" + playerNum));
    }


}
