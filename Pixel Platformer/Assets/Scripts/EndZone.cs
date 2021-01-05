namespace PixelPlatformer
{
    using UnityEngine;
    using UnityEngine.UI;

    public class EndZone : MonoBehaviour
    {
        [SerializeField]
        private Stopwatch _stopWatch;
        [SerializeField]
        private Text _finalMessageText;
        [SerializeField]
        private Screamer _screamer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_stopWatch.CurrentTime < 60000)
            {
                _finalMessageText.text = "SNEAKY FAST!!! CONGRATULATIONS";
                var particles = GetComponent<ParticleSystem>();
                particles.Play();
                _screamer.PlayEndSong();
            }
            else
            {
                _finalMessageText.text = "Try to finish in under a minute. press 'r' to restart.";
            }
            
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
            _stopWatch.StopTimer();
        }
    }
}
