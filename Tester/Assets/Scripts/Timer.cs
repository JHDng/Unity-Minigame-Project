using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] float levelTimer;
    [SerializeField] AudioSource audioSource;
    bool gameOver = false;
    
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
        else if(!gameOver)
        {
            gameOver = true;
            levelTimer = 0;
            Time.timeScale = 0;
            StopAllAudio();
            audioSource.Play();
            gameOverScreen.SetActive(true);
        }
        int minutes = Mathf.FloorToInt(levelTimer / 60);
        int seconds = Mathf.FloorToInt(levelTimer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void StopAllAudio() {
        var allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audioS in allAudioSources) {
            audioS.Stop();
        }
    }
}

