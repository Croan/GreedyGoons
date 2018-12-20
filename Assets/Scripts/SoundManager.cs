using UnityEngine;
using System.Collections;

//order relates to Awake SetBGM
public enum TrackID
{
    BGM_FORGOTTENWINDS,
    BGM_BATTLE,
    BGM_MENU,
    BGM_LOBBY
}

public class SoundManager : MonoBehaviour {

    // singleton
    public static SoundManager Instance;

    // volume control
    public float volumeBGM; //of overall BGM
    public float volumeSFX; //of overall SFX
    
    // All Music
    /* public AudioClip highlightAudio;
    public AudioClip battleBGM; */

    // private members
    private AudioSource[] bgm;
    private float[] vol; //volume multiplier of individual bgm;

    // All SFX
    /* private AudioSource captureSFX;
    public AudioClip captureS; */
	public AudioClip sfxCroc;
	public AudioClip [] sfxDeath;
	public AudioClip sfxGrab;
	public AudioClip [] sfxHurt;
	public AudioClip sfxMummy;
	public AudioClip sfxPunch;
	public AudioClip sfxSpit;
	public AudioClip sfxSpitter;
	public AudioClip sfxStep;
	public AudioClip sfxTreasure;

    private int trackOnPlay;
    private int trackFadeIn;
    private int trackFadeOut;
    private float fadeTime;
    private bool isFade;

    // initializations
    void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        volumeBGM = 0.6f;
        volumeSFX = 0.6f;
        trackOnPlay = -1;
        trackFadeIn = -1;
        trackFadeOut = -1;
        fadeTime = 1;
        isFade = false;

		SetBGM (); /* fieldBGM, battleBGM, menuBGM, lobbyBGM); */ //order relates to enum TrackID
        //VolInit();
        transform.position = GameObject.Find("Main Camera").transform.position;
    }

    

    //=== PRIVATE FUNCTIONS ===
    //- Update  : fade mechanic
    //- SetBGM  : AudioSource bgm initialization
    //- VolInit : individual volumeBGM multiplier initialization

    private void Update()
    {
        if (isFade)
        {
            float deltaVolumeBGM = (Time.deltaTime / fadeTime) * volumeBGM;
            if (trackFadeIn >= 0)
            {
                bgm[trackFadeIn].volume += deltaVolumeBGM;
				if (bgm[trackFadeIn].volume >= volumeBGM) // * vol[trackFadeIn])
                {
					bgm [trackFadeIn].volume = volumeBGM; // * vol[trackFadeIn];
                    trackFadeIn = -1;
                }
            }
            if (trackFadeOut >= 0)
            {
                bgm[trackFadeOut].volume -= deltaVolumeBGM;
                if (bgm[trackFadeOut].volume <= 0)
                {
                    bgm[trackFadeOut].Pause();
                    trackFadeOut = -1;
                }
            }
            if (trackFadeIn < 0 && trackFadeOut < 0) isFade = false;
        }
    }

    private void SetBGM(params AudioClip[] audio) //input: BGMList
    {
        bgm = new AudioSource[audio.Length];
        for(int i=0; i<bgm.Length; i++)
        {
            bgm[i] = GameObject.Find("SoundManager").AddComponent<AudioSource>();
            bgm[i].loop = true;
            bgm[i].clip = audio[i];
        }
        /* captureSFX = GameObject.Find("SoundManager").AddComponent<AudioSource>();
        captureSFX.loop = true;
        captureSFX.clip = captureS; */
    }

    private void VolInit()
    {
        vol = new float[bgm.Length];
        vol[(int)TrackID.BGM_FORGOTTENWINDS] = 0.1f;
        vol[(int)TrackID.BGM_BATTLE] = 1.0f;
        vol[(int)TrackID.BGM_MENU] = 0.5f;
        vol[(int)TrackID.BGM_LOBBY] = 0.3f;
    }

    //=== PUBLIC FUNCTIONS ===
    //- NowPlaying : returns playing track ID
    //- PlayBGM    : play a BGM, stop other BGMs
    //- SwitchBGM  : gradually switch BGMs
    //- FadeIn     : gradually play BGM
    //- FadeOut    : gradually stop BGM
    //- StopAllBGM : stop all playing BGM
    //- PlaySFX    : play one shot audio clip
    //- PlaySFXTransition : play sfx with no scene interruption

    public int NowPlaying()
    {
        return trackOnPlay;
    }

    public void PlayBGM(int track) //input: TrackID
    {
        if (track < 0 || track >= bgm.Length) track = -1;
        StopAllBGM();
        trackOnPlay = track;
        if (track != -1)
        {
			bgm [track].volume = volumeBGM * vol[track];
            bgm[track].Play();
        }
    }

    public void SwitchBGM(int trackSelect, float t) //input: TrackID, fadeTime
    {
        
        if (trackOnPlay == -1) PlayBGM(trackSelect);
        else if (trackOnPlay != trackSelect)
        {
            FadeOut(trackOnPlay, t);
            if (!bgm[trackSelect].isPlaying)
            {
                bgm[trackSelect].volume = 0;
                bgm[trackSelect].Play();
            }
            FadeIn(trackSelect, t);
        }
    }

    public void FadeIn(int track, float t) //input: TrackID, fadeTime
    {
        if (t <= 0) t = 1;
        if (track < 0 || track >= bgm.Length) track = 0;
        bgm[track].UnPause();
        trackOnPlay = track;
        trackFadeIn = track;
        fadeTime = t;
        isFade = true;
    }

    public void FadeOut(int track, float t) //input: TrackID, fadeTime
    {
        if (t <= 0) t = 1;
        if (track < 0 || track >= bgm.Length) track = 0;
        trackFadeOut = track;
        fadeTime = t;
        isFade = true;
    }

    public void StopAllBGM()
    {
        for(int i=0; i<bgm.Length; i++)
            bgm[i].Stop();
        trackOnPlay = -1;
    }

	// Use this to play SFX from any script
	public void PlaySFX(AudioClip audio, float volume = -1) {
		if (volume == -1) volume = volumeSFX;
		else volume *= volumeSFX;
		AudioSource.PlayClipAtPoint(audio, GameObject.Find("Main Camera").transform.position, volume);
	}

	public void PlaySFX(AudioClip audio, Vector3 position, float volume = -1) {
        if (volume == -1) volume = volumeSFX;
        else volume *= volumeSFX;
		AudioSource.PlayClipAtPoint(audio, position, volume);
    }

	public void PlaySFXFromList(AudioClip [] audio, float volume = -1) {
		if (volume == -1) volume = volumeSFX;
		else volume *= volumeSFX;
		int randy = (int)(Random.value * audio.Length);
		AudioSource.PlayClipAtPoint(audio[randy], GameObject.Find("Main Camera").transform.position, volume);
	}

	public void PlaySFXFromList(AudioClip [] audio, Vector3 position, float volume = -1) {
		if (volume == -1) volume = volumeSFX;
		else volume *= volumeSFX;
		int randy = (int)(Random.value * audio.Length);
		AudioSource.PlayClipAtPoint(audio[randy], position, volume);
	}

    public void PlaySFXTransition(AudioClip audio, float volume = 1.0f)
    {
        AudioSource test = GameObject.Find("SoundManager").AddComponent<AudioSource>();
        test.clip = audio;
        test.volume = volume * volumeSFX;
        test.Play();
        Destroy(test, audio.length);
    }

    /* public void PlaySFX_Victory()
    {
        PlaySFX(victoryS, 0.7f);
    }
    public void PlaySFX_Defeat()
    {
        PlaySFX(defeatS);
    }
    public void PlaySFX_Respawn()
    {
        PlaySFXTransition(respawnS);
    }
    public void PlaySFX_HillSpawn()
    {
        PlaySFXTransition(hillSpawnS, 0.6f);
    }
    public void PlaySFX_HillPrepare()
    {
        PlaySFXTransition(prepareS);
    }

    public void PlayCaptureSFX()
    {
        captureSFX.volume = volumeSFX;
        captureSFX.Play();
    }
    public void StopCaptureSFX()
    {
		if(captureSFX.clip == captureS)
        	captureSFX.Stop();
    }
	public void PlayPointSFX () {
		Debug.Log ("PlayPointSFX");
		captureSFX.clip = pointS;
		captureSFX.Play();
	}
	public void StopPointSFX () {
		captureSFX.Stop ();
		captureSFX.clip = captureS;
	} */

	// Works with Volume Slider UI
    /* public void UpdateBGMVolume()
    {
        volumeBGM = GameObject.Find("SliderBGM").GetComponent<UnityEngine.UI.Slider>().value;
        if (trackOnPlay >= 0)
            bgm[trackOnPlay].volume = vol[trackOnPlay] * volumeBGM;
    }

    public void UpdateSFXVolume()
    {
        volumeSFX = GameObject.Find("SliderSFX").GetComponent<UnityEngine.UI.Slider>().value;
    } */
    /*
    public void OnGUI()
    {
        bgm[trackOnPlay].volume = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), bgm[trackOnPlay].volume, 0.3F, 0.9F);
    }
    */
}
