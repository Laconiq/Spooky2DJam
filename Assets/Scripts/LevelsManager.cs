using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private TextMeshProUGUI levelText;
    private int _currentLevel;
    private GameObject _vampireCharacter;
    private GameObject _batCharacter;
    private void Awake()
    {
        _vampireCharacter = FindObjectOfType<PlayerController>().gameObject;
        _batCharacter = FindObjectOfType<BatController>().gameObject;
        _currentLevel = 0;
        MoveCharactersToCurrentLevel();
        UpdateLevelText();
    }
    public void MoveCharactersToCurrentLevel()
    {
        if (_currentLevel >= levels.Count) return;
        Level currentLevel = levels[_currentLevel];
        _vampireCharacter.transform.position = currentLevel.vampireSpawn.transform.position;
        _batCharacter.transform.position = currentLevel.batSpawn.transform.position;
        UpdateLevelText();
    }
    private void GoToLevel(int i)
    {
        if (i < 0 || i >= levels.Count) return;
        _currentLevel = i;
        MoveCharactersToCurrentLevel();
    }
    public void GoToNextLevel()
    {
        if (_currentLevel >= levels.Count - 1) return;
        _currentLevel++;
        MoveCharactersToCurrentLevel();
    }

    private void UpdateLevelText()
    {
        int maxLevel = levels.Count;
        levelText.text = "Level " + (_currentLevel + 1) + "/" + maxLevel;
    }
}