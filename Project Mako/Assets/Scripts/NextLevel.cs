using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextLevel : MonoBehaviour
{
    private Collider hitBox;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Awake()
    {
        hitBox = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            FinishLevel();
        }
    }
    private void FinishLevel()
    {
        Time.timeScale = 0;
        gameOverText.text = "Congratulations! You've completed the demo level!";
        gameoverPanel.SetActive(true);
    }
}
