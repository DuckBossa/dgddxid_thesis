using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitializationSceneHandler : MonoBehaviour {

    static AudioSource audio;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        SceneManager.LoadScene(1);
    }
}
