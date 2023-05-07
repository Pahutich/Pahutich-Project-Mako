using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool GameIsPaused { get; private set; } = false;
    private GameObject player;
    private Health playerHealth;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI pauseOrLossText;
    PlayerInputActions playerInputActions;
    private const string PauseText = "Pause";
    private const string LossText = "You lost";
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        gameOverPanel.SetActive(false);
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealth.GetHealthSystem().OnDead += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        bool pauseTime = playerInputActions.Player.Pause.triggered;
        if (pauseTime)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!GameIsPaused)
        {
            GameIsPaused = true;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            pauseOrLossText.text = PauseText;
            Cursor.visible = true;
        }
        else
        {
            GameIsPaused = false;
            Time.timeScale = 1;
            gameOverPanel.SetActive(false);
            Cursor.visible = false;
        }
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        pauseOrLossText.text = LossText;
        pauseOrLossText.color = Color.red;
        gameOverPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
