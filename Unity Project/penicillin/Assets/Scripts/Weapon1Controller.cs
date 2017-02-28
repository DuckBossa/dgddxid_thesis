using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GLOBAL;

public class Weapon1Controller : MonoBehaviour {
    public RectTransform content, wlv1, wlv2, wlv3;
    public Image lv3;
	public PlayerAttack pa;
	public int weapType;


    private Image[] wps;
    private int cur;
    private int distBetButtons;
    private Vector2 newpos;
    private bool moving;
    private int lvl;
    private CanvasRenderer col;
    private float movingTimer;

    // Check for costs and disable applicable upgrades
    public void CheckAvailability(int rp) {

    }

    void Start() {
        wps = new Image[3]; //store the buttons, pls don't mess with the arrangement of the buttons in the editor
        for (int i = 0; i < wps.Length; i++) {
            wps[i] = content.GetChild(i).gameObject.GetComponent<Image>();
        }

        cur = 0; // current weapon

        distBetButtons = (int)wps[1].transform.position.y - (int)wps[0].transform.position.y;
        newpos = content.position;
        col = this.GetComponent<CanvasRenderer>();
        lvl = 0;
    }

    
    void Update() {
        if(lvl == 3) { //if level = 3 just dim everything then disable
            col.SetAlpha(Mathf.Lerp(col.GetAlpha(), 0, 10 * Time.unscaledDeltaTime));
            for(int i = 0; i < 6; i++) {
                content.GetChild(i).GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(content.GetChild(i).GetComponent<CanvasRenderer>().GetAlpha(), 0, 10 * Time.unscaledDeltaTime));
            }
        }

        if (moving) {
            movingTimer += Time.unscaledDeltaTime;
            content.position = Vector2.Lerp(content.position, newpos, 10 * Time.unscaledDeltaTime);
            moving = movingTimer > .5f ? false : true;
            if (lvl == 3 && !moving) {
                lv3.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Upgrade() {
		if (ScoreManager.researchPoints >= GAME.RP_UPGRADE [pa.GetWeapLevel (weapType)]) {
			if (!moving) {
				movingTimer = 0;
				lvl++; // current weapon lvl, 0 means not purchased

				// upgrade weapon
				pa.UpgradeWeapon(weapType);
				// change research points accordingly
				ScoreManager.researchPoints -= GAME.RP_UPGRADE[pa.GetWeapLevel(weapType)];


				// move objects
				newpos = lvl == 3 ? new Vector2(content.position.x, content.position.y - distBetButtons / 2) : new Vector2(content.position.x, content.position.y - distBetButtons);
				moving = true;
				// remove the locked icon
				if (lvl == 1) wlv1.GetChild(0).GetComponent<Image>().enabled = false;
				else if (lvl == 2) wlv2.GetChild(0).GetComponent<Image>().enabled = false;
				else if (lvl == 3) wlv3.GetChild(0).GetComponent<Image>().enabled = false;

			}
		}
        
    }
}
