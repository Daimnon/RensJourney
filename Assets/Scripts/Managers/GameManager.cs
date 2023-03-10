using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Town, Merchant, Combat, Map}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private GameObject _playerPrefab, _allyPlaceholderPrefab, _enemyPlaceholderPrefab;

    private Player _playerCharacter;
    public Player PlayerCharacter => _playerCharacter;

    [SerializeField] private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }


    public event Action OnStartGame, OnStartCombat, OnEndCombat, OnEndGame;

    private void Awake()
    {
        _instance = this;
        _playerCharacter = _playerPrefab.GetComponent<Player>();
        DontDestroyOnLoad(this);
    }

    public void InvokeStartGame() // occurs when entering combat.
    {
        SaveManager.Instance.LoadData();
        OnStartGame?.Invoke();
        //StartCoroutine(LoadInTheNextFrame());
    }
    public void InvokeStartCombat() // occurs when entering combat.
    {
        SaveManager.Instance.SaveData();
        OnStartCombat?.Invoke();
        StartCoroutine(LoadInTheNextFrame());
    }
    public void InvokeEndCombat() // occurs if player survived the combat and all enemies are dealt with.
    {
        SaveManager.Instance.SaveData();
        OnEndCombat?.Invoke();
        StartCoroutine(LoadInTheNextFrame());
    }
    public void InvokeEndGame() // occurs when entering combat.
    {
        SaveManager.Instance.SaveData();
        OnEndGame?.Invoke();
    }

    private IEnumerator LoadInTheNextFrame()
    {
        yield return null;

        SaveManager.Instance.LoadData();
    }
}
