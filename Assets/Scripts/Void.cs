using UnityEngine;
public class Void : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController == null) return;
        LevelsManager levelsManager = FindObjectOfType<LevelsManager>();
        if (levelsManager == null) return;
        levelsManager.MoveCharactersToCurrentLevel();
    }
}