using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private Animator anim;
    private bool chestOpened;
    public bool open;

    private void Start()
    {
        chestOpened = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
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

    public void SetOpen(bool open)
    {
        this.open = open;
    }

    public bool GetChestOpened()
    {
        return chestOpened;
    }
}
