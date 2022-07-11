using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    public Score score;
    public TextMeshProUGUI scoreText;
    private string filePath;

    private void Start()
    {
#if UNITY_EDITOR
        filePath = Application.dataPath + "/save.txt";
#endif
#if UNITY_ANDROID
        filePath = Application.persistentDataPath + "/save.txt";
#endif
#if UNITY_STANDALONE_WIN
        filePath = Application.dataPath + "/save.txt";
#endif
        if (File.Exists(filePath))
        {
            string saveString = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);
            score.score = saveData.score;
            Debug.Log("Score: " + score);
        }

        ShowScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowScore()
    {
        scoreText.text = "Highest score: " + score.score;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public class SaveData
    {
        public int score;
    }
}