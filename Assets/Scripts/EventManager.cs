using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public AK.Wwise.Event coinPickup;
    public AK.Wwise.Event crocCry;
    public AK.Wwise.Event crocGrab;
    public AK.Wwise.Event mummyCry;
    public AK.Wwise.Event playerHurt;
    public AK.Wwise.Event playerPunch;
    public AK.Wwise.Event playerStep;
    public AK.Wwise.Event playerSwim;
    public AK.Wwise.Event spitterCry;
    public AK.Wwise.Event startMusic;

    // Start is called before the first frame update
    void Start() {
        AkBankManager.LoadBank("Main", false, false);
        startMusic.Post(gameObject);
    }

    void Awake() {
        if (!Instance)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
