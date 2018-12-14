using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    private bool calm;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public static GameController Instance
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
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1;
        PlayerController.Instance.Reset();
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(0, 0, -35f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        if (!GameOver)
        {
            if (PlayerController.Instance.isDead())
            {
                EndGame();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (GamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if (CheckChargingEnemies())
            {
                if (calm)
                {
                    calm = false;
                    SoundController.Instance.PlayActionMusic();
                }
            }
            else
            {
                if (!calm)
                {
                    calm = true;
                    SoundController.Instance.PlayCalmMusic();
                }
            }
        }
    }

    private bool CheckChargingEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Foe");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<AIEnemyStateController>().state == AIEnemyStateController.State.Charge)
            {
                return true;
            }

        }
        return false;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        SoundController.Instance.PlayMenuMusic();
        GamePaused = true;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        SoundController.Instance.PlayMenuMusic();
        GameOver = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        if (calm)
        {
            SoundController.Instance.PlayCalmMusic();
        }
        else
        {
            SoundController.Instance.PlayActionMusic();
        }
        GamePaused = false;
    }

    public bool GamePaused { get; set; }

    public bool GameOver { get; set; }

    public void Quit()
    {
        SoundController.Instance.Stop();
    }
}
