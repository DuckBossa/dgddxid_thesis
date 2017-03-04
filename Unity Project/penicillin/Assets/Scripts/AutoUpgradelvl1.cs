using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoUpgradelvl1 : MonoBehaviour {
	public Canvas thing;
    public GameObject score; //score manager

    Weapon1Controller wp;
    private ScoreManager smgr;
	void Start()
	{
        wp = GetComponent<Weapon1Controller>();
        Time.timeScale = 0;
		StartCoroutine(LateStart(0.5f));
        smgr = score.GetComponent<ScoreManager>();
    }

	IEnumerator LateStart(float waitTime)
	{
        wp.Upgrade();
        smgr.researchPoints = 0;
        Time.timeScale = 1;
        thing.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);

	}
}
