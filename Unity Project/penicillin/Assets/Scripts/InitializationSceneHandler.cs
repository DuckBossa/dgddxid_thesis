using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitializationSceneHandler : MonoBehaviour {

    static AudioSource audio;
    public AudioClip buttonSound;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = buttonSound;
        SceneManager.LoadScene(1);
    }
}
