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
        public string FormattedCurrentTime { get; private set; }

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
            StartCoroutine(BlinkCharacter(this.gameObject));
        }
        public IEnumerator BlinkCharacter(GameObject player)
        {
            var sprites = player.GetComponent<Text>();
            bool blink = false;
            float blinkTime = 4f;
            while (blinkTime > 0)
            {
                blink = !blink;
                if (blink)
                {
                    sprites.enabled = false;
                }
                else
                {
                    sprites.enabled = true;
                }
                blinkTime -= .1f;
                yield return new WaitForSeconds(.1f);
            }

            
            sprites.enabled = true;
            
        }
        IEnumerator Run()
        {
            int millis = 60000;
            int currentTime = 0;
            while (!breaker)
            {
                millis -= 10;
                currentTime += 10;

                double totalMilliseconds = millis;
                var totalSeconds = totalMilliseconds / 1000;

                var seconds = totalSeconds % 60;
                CurrentTime = currentTime;
                if(millis > 0)
                {
                    FormattedCurrentTime = $"{seconds.ToString("00.00")}";
                    text.text = FormattedCurrentTime;
                }
                
                yield return new WaitForSeconds(.01f);
            }
        }
    }
}