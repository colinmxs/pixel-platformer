namespace PixelPlatformer
{
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class CameraYSetter : MonoBehaviour
    {
        [SerializeField]
        private float YPos;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerController>(out _))
            {
                var cameraController = Camera.main.GetComponent<CameraController>();
                cameraController.constYPos = YPos;
            }
        }
    }
}