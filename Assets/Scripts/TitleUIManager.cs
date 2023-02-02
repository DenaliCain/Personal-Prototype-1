using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using UnityEngine;
// Delays execution, good practice to delay UI related stuff
[DefaultExecutionOrder(1000)]

//Practicing implementing managers and writing good scripts
public class TitleUIManager : MonoBehaviour
{
    public DifficultyPicker DifficultyPicker;
    //code in NewDifficultySelected is to be executed when a difficulty is selected
    public void NewDifficultySelected(string difficulty)
    {
        MainManager.Instance.NewDifficulty = difficulty;
    }
    // Start is called before the first frame update
    private void Start()
    {
        DifficultyPicker.Init();
        DifficultyPicker.onDifficultyChanged += NewDifficultySelected;
    }
    public void StartNew()
    {
        Debug.Log(MainManager.Instance.NewDifficulty);
        SceneManager.LoadScene(1);
        
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
