using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Player player;
    private Camera mainCamera;

    private void Awake()
    {
        player = GetComponent<Player>();
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        mainCamera.transform.position = playerPos;
    }
}
