using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;

    [SerializeField]
    Text levelText;

    private void Start()
    {
        levelText.text = levelNumber.ToString();
        if(PlayerPrefs.GetInt("playerLevel") < levelNumber)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("menuCount") + levelNumber - 1);
    }
}
