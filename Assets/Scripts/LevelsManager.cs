using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private TextMeshProUGUI levelText;
    private int _currentLevel;
    private GameObject _vampireCharacter;
    private GameObject _batCharacter;
    public int startLevel;
    [SerializeField] private GameObject credits;

    private void Awake()
    {
        _currentLevel = startLevel - 1;
        _vampireCharacter = FindObjectOfType<PlayerController>().gameObject;
        _batCharacter = FindObjectOfType<BatController>().gameObject;
        MoveCharactersToCurrentLevel();
        UpdateLevelText();
    }

    public void MoveCharactersToCurrentLevel()
    {
        if (_currentLevel >= levels.Count) return;
        Level currentLevel = levels[_currentLevel];
        if (_batCharacter.GetComponent<LightController>().playerState == LightController.PlayerState.Interacting)
            _batCharacter.GetComponent<LightController>().lightSwitch.MechanismActivation(_batCharacter);
        if (_vampireCharacter.GetComponent<LightController>().playerState == LightController.PlayerState.Interacting)
            _vampireCharacter.GetComponent<LightController>().lightSwitch.MechanismActivation(_vampireCharacter);
        _vampireCharacter.transform.position = currentLevel.vampireSpawn.transform.position;
        _batCharacter.transform.position = currentLevel.batSpawn.transform.position;
        UpdateLevelText();
        _vampireCharacter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        RaycastData[] raycastDataObjects = FindObjectsOfType<RaycastData>();
        foreach (RaycastData raycastData in raycastDataObjects)
        {
            raycastData.ResetRotation();
        }
    }

    private void GoToLevel(int i)
    {
        if (i < 0 || i >= levels.Count) return;
        _currentLevel = i;
        MoveCharactersToCurrentLevel();
    }

    public void GoToNextLevel()
    {
        if (_currentLevel >= levels.Count - 1)
        {
            Debug.Log("Affichage des crédits !");
            credits.SetActive(true);
            StartCoroutine(ReturnToMainMenuAfterDelay());
        }
        else
        {
            _currentLevel++;
            MoveCharactersToCurrentLevel();
            Debug.Log("Passage au niveau suivant : " + (_currentLevel + 1));
        }
    }

    private void UpdateLevelText()
    {
        int maxLevel = levels.Count;
        levelText.text = "Level " + (_currentLevel + 1) + "/" + maxLevel;
    }

    private IEnumerator ReturnToMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
