using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public Score score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (score)
            {
                GameManager.Instance.score++;
                GameManager.Instance.scoreText.text = "Score: " + GameManager.Instance.score;
                if (GameManager.Instance.score > score.score)
                {
                    score.score = GameManager.Instance.score;
                }

                if (GameManager.Instance.score % 10 == 0)
                {
                    GameManager.Instance.movementSpeed++;
                }
            }
        }
    }
}