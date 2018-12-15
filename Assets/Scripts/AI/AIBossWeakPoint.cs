using UnityEngine;
using System.Collections;

public class AIBossWeakPoint : MonoBehaviour
{
    public enum WeakPoint { RightLeg, RightArm, LeftArm };

    public WeakPoint weakPoint;
    private bool destroyed = false;

    public int hitsRemaining = 3;
    public int attackValue;
    public string attackAnimationTrigger;

    private GameObject boss;
    private AIBossStateController BossStateControllerScript;

    void Start()
    {
        boss = GameObject.FindWithTag("Boss");
        BossStateControllerScript = boss.GetComponent<AIBossStateController>();

        BossStateControllerScript.RegisterWeakPoint(gameObject);
    }

    void Update()
    {   
        if (Input.GetKeyUp("k") && weakPoint==WeakPoint.RightArm){
            hitsRemaining--;
        }


        if (hitsRemaining == 0 && !destroyed)
        {   
            destroyed = true;
            BossStateControllerScript.RemoveWeakPoint(gameObject);
            transform.localScale = new Vector3(0, 0, 0);
        }
    }


    public int GetAttackValue()
    {
        return attackValue;
    }
}
