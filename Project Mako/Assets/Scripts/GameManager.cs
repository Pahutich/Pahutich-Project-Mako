using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Health playerHealth;
    [SerializeField] private GameObject gameOverPanel;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        gameOverPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerHealth.GetHealthSystem().OnDead += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }
}
