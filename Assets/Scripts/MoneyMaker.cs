using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMaker : MonoBehaviour {

    private static int countCollected = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            countCollected += 1;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxTreasure, transform.position, 0.7f);
            Destroy(gameObject);
        }
    }
}
