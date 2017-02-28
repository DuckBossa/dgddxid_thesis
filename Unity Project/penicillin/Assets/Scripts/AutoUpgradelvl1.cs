using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoUpgradelvl1 : MonoBehaviour {
	public Canvas thing;
	void Start()
	{
		Time.timeScale = 0;
		StartCoroutine(LateStart(0.5f));
	}

	IEnumerator LateStart(float waitTime)
	{
		
		GetComponent<Weapon1Controller> ().Upgrade ();
		ScoreManager.researchPoints = 0;
		Time.timeScale = 1;
		thing.gameObject.SetActive (false);
		yield return new WaitForSeconds(waitTime);

	}
}
