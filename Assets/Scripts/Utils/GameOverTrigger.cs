using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Game Over Triggered by Monster");
            GameManager.Instance.GameOver();
        }
    }
}