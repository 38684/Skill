using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform playerPostition;

    private void Update()
    {
        transform.position = new Vector3(playerPostition.position.x, playerPostition.position.y, -10);
    }
}
