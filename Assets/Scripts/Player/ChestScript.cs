using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

    Animator anim;
    public bool open;

    private bool chestOpened;

    // Use this for initialization
    void Start () {
        chestOpened = false;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            anim.SetBool("OpenChest", open);
            chestOpened = true;
        }
		
	}
    public bool GetOpen()
    {
        return open;
    }
    public void SetOpen(bool o)
    {
        open = o;
    }
    public bool GetChestOpened()
    {
        return chestOpened;
    }
}
