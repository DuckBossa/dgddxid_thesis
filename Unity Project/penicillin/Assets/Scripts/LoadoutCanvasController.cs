using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadoutCanvasController : MonoBehaviour {

    public RectTransform p;
    public Button[] buttons;
    public Transform center;
    int currButton = 0;
    int distBetButtons;
    bool drag;
    // Use this for initialization
    void Start() {
        gameObject.SetActive(false);
        drag = false;
        distBetButtons = (int)Mathf.Abs(buttons[0].GetComponent<RectTransform>().anchoredPosition.x - buttons[1].GetComponent<RectTransform>().anchoredPosition.x);
    }

    // Update is called once per frame
    void Update() {
        float newx = Mathf.Lerp(p.anchoredPosition.x, currButton * -distBetButtons, Time.unscaledDeltaTime * 10f);
        Vector2 newpos = new Vector2(newx, p.anchoredPosition.y);
        p.anchoredPosition = newpos;
    }

    public void Drag() {
        drag = !drag;
    }

    public void Next() {
        currButton = (currButton + 1) % buttons.Length;
    }

    public void Previous() {
        currButton = (currButton + buttons.Length - 1) % buttons.Length;
    }

    public int getCurrImageID() {
        return currButton;
    }
}
