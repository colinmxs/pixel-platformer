namespace PixelPlatformer
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Restarter : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}