using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    Text totalEnemyKilled;
    void Start()
    {
        Time.timeScale = 1f;
        if (!PlayerPrefs.HasKey("enemyKilled"))
        {
            PlayerPrefs.SetInt("enemyKilled", 0); 
        }
        if (!PlayerPrefs.HasKey("menuCount"))
        {
            PlayerPrefs.SetInt("menuCount", 2);
        }
        if (!PlayerPrefs.HasKey("playerLevel"))
        {
            PlayerPrefs.SetInt("playerLevel", 1);
        }
    }

    private void Update()
    {
        totalEnemyKilled.text = PlayerPrefs.GetInt("enemyKilled").ToString();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
