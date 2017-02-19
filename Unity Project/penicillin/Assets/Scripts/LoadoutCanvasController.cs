using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadoutCanvasController : MonoBehaviour {

    public RectTransform p;
    public Button[] buttons;
	public Sprite[] normal;
	public Sprite[] upgraded;
	public Sprite[] final;
    public Transform center;
	public PlayerAttack pa;
    int currButton = 0;
    int distBetButtons;
    bool drag;
    // Use this for initialization
    void Start() {
        gameObject.SetActive(false);
        drag = false;
		UpdateButtonImages ();
        distBetButtons = (int)Mathf.Abs(buttons[0].GetComponent<RectTransform>().anchoredPosition.x - buttons[1].GetComponent<RectTransform>().anchoredPosition.x);
    }

    // Update is called once per frame
    void Update() {
        float newx = Mathf.Lerp(p.anchoredPosition.x, currButton * -distBetButtons, Time.unscaledDeltaTime * 10f);
        Vector2 newpos = new Vector2(newx, p.anchoredPosition.y);
        p.anchoredPosition = newpos;
    }

	public void UpdateButtonImages(){
		for (int i = 0; i < buttons.Length; i++) {
			int weapLevel = pa.GetWeapLevel (i);
			switch (weapLevel) {
			case 0:
				buttons [i].GetComponent<Image> ().sprite = normal [i];
				break;
			case 1:
				buttons [i].GetComponent<Image> ().sprite = upgraded [i];
				break;
			case 2:
				buttons [i].GetComponent<Image> ().sprite = final [i];
				break;
			}
			//buttons[i].image =
		}
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
