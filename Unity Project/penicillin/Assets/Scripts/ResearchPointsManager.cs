using UnityEngine;
using System.Collections;
using GLOBAL;
using UnityEngine.UI;

public class ResearchPointsManager : MonoBehaviour {

    // Use this for initialization
    public Text rphud, rplab;
    public int rpcurrent, rptotal;
    public Slider enemyCountSlider;
	void Start () {
        rpcurrent = 0;
        rptotal = 0;
        rphud.text = rpcurrent.ToString("D6");
        rplab.text = rpcurrent.ToString("D6");
    }

    public void Addpoints(int val) {
        rpcurrent += val;
        rptotal += val;
        rphud.text = rpcurrent.ToString("D6");
        rplab.text = rpcurrent.ToString("D6");
        enemyCountSlider.value++;
    }

    public void Deductpoints(int val) {
        rpcurrent -= val;
        rptotal -= val;
        rphud.text = rpcurrent.ToString("D6");
        rplab.text = rpcurrent.ToString("D6");
    }
}
