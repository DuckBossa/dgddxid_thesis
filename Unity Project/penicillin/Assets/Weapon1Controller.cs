using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon1Controller : MonoBehaviour {
    public RectTransform content;
    public Image lv3;

    private Button[] wps;
    private int cur;
    private int distBetButtons;
    private Vector2 newpos;
    private bool moving;
    private int lvl;
    private CanvasRenderer col;

    // Check for costs and disable applicable upgrades
    public void CheckAvailability(int rp) {

    }

    void Start() {
        wps = new Button[3]; //store the buttons, pls don't mess with the arrangement of the buttons in the editor
        for (int i = 0; i < wps.Length; i++) {
            wps[i] = content.GetChild(i).gameObject.GetComponent<Button>();
        }

        cur = 0; // current weapon

        distBetButtons = 235;
        newpos = content.position;
        col = this.GetComponent<CanvasRenderer>();
    }

    
    void Update() {
        if(lvl == 3) {
            col.SetAlpha(Mathf.Lerp(col.GetAlpha(), 0, 10 * Time.unscaledDeltaTime));
            for(int i = 0; i < 6; i++) {
                content.GetChild(i).GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(content.GetChild(i).GetComponent<CanvasRenderer>().GetAlpha(), 0, 10 * Time.unscaledDeltaTime));
            }
        }
        if (moving) {
            content.position = Vector2.Lerp(content.position, newpos, 10 * Time.unscaledDeltaTime);
            moving = (int)newpos.y == (int)content.position.y ? false : true;
            //col.SetAlpha(lvl == 3 ? Mathf.Lerp(col.GetAlpha(), 0, 10 * Time.unscaledDeltaTime) : 1);
            if(lvl == 3 && !moving) {
                lv3.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Upgrade() {
        if (!moving) {
            // deactivate button, update weapon (just a counter of how many times upgrade was clicked here, to avoid confusion)
            wps[cur++].interactable = false;
            lvl++;

            // move objects
            newpos =  lvl == 3 ? new Vector2(content.position.x, content.position.y - distBetButtons/2) : new Vector2(content.position.x, content.position.y - distBetButtons);
            moving = true;

            // upgrade weapon

            // change research points accordingly

        }
    }
}
