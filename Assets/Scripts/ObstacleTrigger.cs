using BirdTools;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    public GameEvent onPause;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (onPause)
            {
                onPause.Raise();
            }
        }
    }
}