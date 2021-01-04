namespace PixelPlatformer
{
    using UnityEngine;

    public class EndZone : MonoBehaviour
    {
        [SerializeField]
        private Stopwatch _stopWatch;
        [SerializeField]
        private Screamer _screamer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var particles = GetComponent<ParticleSystem>();
            particles.Play();
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
            _stopWatch.StopTimer();
            _screamer.PlayEndSong();
        }
    }
}
