using BirdTools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public GameEvent onStart;
    public GameEvent onPause;
    public GameEvent onResume;
    public int score = 0;
    public Score scoreSo;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lostScoreText;
    public int movementSpeed = 10;
    public string filepath;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
#if UNITY_EDITOR
        filepath = Application.dataPath + "/save.txt";
#endif
#if UNITY_ANDROID
        filepath = Application.persistentDataPath + "/save.txt";
#endif
#if UNITY_STANDALONE_WIN
        filepath = Application.dataPath + "/save.txt";
#endif
        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (onStart)
        {
            onStart.Raise();
        }
    }

    // save score data to json file
    private void Save()
    {
        SaveData saveData = new SaveData
        {
            score = scoreSo.score
        };
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filepath, json);
        Debug.Log("File Saved!");
    }

    // load saved high score from json file
    public void Load()
    {
        if (File.Exists(filepath))
        {
            string saveString = File.ReadAllText(filepath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);
            scoreSo.score = saveData.score;
        }
    }

    public void OnPause()
    {
        if (onPause)
        {
            onPause.Raise();
        }

        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;

        if (onResume)
        {
            Debug.Log(onResume);

            onResume.Raise();
        }
    }

    public void OnLose()
    {
        lostScoreText.text = "Your Score: " + score;
        Save();
        Time.timeScale = 0f;
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public class SaveData
    {
        public int score;
    }
}