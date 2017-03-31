using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GLOBAL;

public class Load_MainMenu : MonoBehaviour {
    public Text msg;
    void Start() {
        msg.text = GAME.messages[Random.Range(0, GAME.messages.Length - 1)];
        SceneManager.LoadScene("MainMenu");
    }
}
