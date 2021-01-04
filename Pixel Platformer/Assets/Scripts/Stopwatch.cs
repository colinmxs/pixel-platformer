namespace PixelPlatformer
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class Stopwatch : MonoBehaviour
    {
        public double CurrentTime { get; set; }

        private Text text;
        private bool breaker = false;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            StartCoroutine(Run());
        }
        internal void StopTimer()
        {
            breaker = true;
        }

        IEnumerator Run()
        {
            int millis = 0;

            while (!breaker)
            {
                millis += 100;

                double totalMilliseconds = millis;
                var totalSeconds = totalMilliseconds / 1000;

                var minutes = Math.Floor(totalSeconds / 60);
                var seconds = totalSeconds % 60;
                CurrentTime = totalMilliseconds;
                text.text = $"{minutes}:{seconds.ToString("00.00")}";
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}