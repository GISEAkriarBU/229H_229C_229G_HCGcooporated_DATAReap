using UnityEngine;

public class MoveTo_B : MonoBehaviour
{
    public Transform target; // The target (empty GameObject) the player teleports to
    public float teleportDelay = 0.1f; // Delay before teleporting (optional)

    private void Update()
    {
        // Check for key press and teleport when the key is pressed
        if (Input.GetKeyDown(KeyCode.S)) // Teleport to target when 'A' is pressed
        {
            TeleportToTarget();
        }
    }

    void TeleportToTarget()
    {
        // Teleport the player to the target position immediately
        transform.position = target.position;
    }
}
