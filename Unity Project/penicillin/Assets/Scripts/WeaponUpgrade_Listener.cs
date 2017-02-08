using UnityEngine;
using System.Collections;

public class WeaponUpgrade_Listener : MonoBehaviour {

    public LoadoutCanvasController LCC;
    public PlayerAttack pa;
    public void upgradeWeap() {
        pa.UpgradeWeapon(LCC.getCurrImageID());
		LCC.UpdateButtonImages ();
    }

}
