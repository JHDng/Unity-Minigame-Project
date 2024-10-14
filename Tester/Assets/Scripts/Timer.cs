using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] float levelTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTimer > 0.1f)
        {
            levelTimer -= Time.deltaTime;
        }
        else
        {
            levelTimer = 0;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
        int minutes = Mathf.FloorToInt(levelTimer / 60);
        int seconds = Mathf.FloorToInt(levelTimer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
