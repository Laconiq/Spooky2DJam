using MoreMountains.Feedbacks;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    [SerializeField] private MMF_Player mmfPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController == null) return;
        LevelsManager levelsManager = FindObjectOfType<LevelsManager>();
        if (levelsManager == null) return;
        mmfPlayer.PlayFeedbacks();
        levelsManager.GoToNextLevel();
    }
}