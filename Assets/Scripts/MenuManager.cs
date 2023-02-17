using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public string playerName;

    public TMP_InputField playerNameTextField;

    public void SetPlayerName()
    {
        playerName = playerNameTextField.text;
    }

    public void StartGame()
    {

        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {

#if UNITY_EDITOR 
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
    
    [Serializable]
    public class HighScore
    {
        public string playerName = "NONAME";
        public int score = 0;

    }
    
    public void SaveData(string playerName, int score)
    {
        HighScore data = new HighScore();

        data.playerName = playerName;
        data.score = score;
        
        
        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public HighScore LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);

            return data;
        }

        return new HighScore();
    }
   
    
}
