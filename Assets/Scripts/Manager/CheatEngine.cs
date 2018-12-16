using UnityEngine;

public class CheatEngine : MonoBehaviour
{
    private static CheatEngine instance;

    public static CheatEngine Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            KillEnemies();
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            RestorePlayerHealth();
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            KillBoss();
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            KillKratos();
        }
    }

    private void KillEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Foe"))
        {
            Destroy(enemy);
        }
    }

    private void KillBoss()
    {
        GameObject boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            Destroy(boss);
        }
    }

    private void KillKratos()
    {
        PlayerController.Instance.KillKratos();
    }

    private void RestorePlayerHealth()
    {
        PlayerController.Instance.RestoreHealth();
    }
}
