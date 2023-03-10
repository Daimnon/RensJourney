using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager Instance => _instance;

    private int _townSceneNum = 0;
    private int _trainingSceneNum = 1;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadTown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_townSceneNum);
    }
    public void LoadCombatScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_trainingSceneNum);
    }
}
