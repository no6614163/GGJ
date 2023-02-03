using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleState
{
    None,
    Open,
    Idle,
    Close,
    Catch
}

public class Mole : MonoBehaviour
{
    public MoleState MS;
    public int plusPoint = 100;
    public int minusPoint = -1000;

    private float waitTime;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        MS = MoleState.None;
        waitTime = Random.Range(0.5f, 4.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        else if (MS == MoleState.None)
        {
            OpenMole();
        }

    }
    void OpenMole(){
		int isGood = Random.Range (0, 100);
		MS = MoleState.Open;
		if (isGood <= goodGuyPercent) {
			anim.SetTrigger ("OpenB");
			MT = MoleType.GoodGuy;
		} else {
			anim.SetTrigger ("OpenA");
			MT = MoleType.BadGuy;
		}
	}

}
