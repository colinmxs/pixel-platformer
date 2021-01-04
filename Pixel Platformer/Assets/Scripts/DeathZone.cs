namespace PixelPlatformer
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    public class DeathZone : MonoBehaviour
    {
        [SerializeField]
        private bool _respawnZombies = true;
        [SerializeField]
        private bool _respawnHumans = true;
        [SerializeField]
        private GameObject _respawn;
        [SerializeField]
        private float _blinkSecs = 1f;

        public UnityEvent OnPlayerTrigger = new UnityEvent();
        public UnityEvent OnZombieTrigger = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_respawnHumans && collision.gameObject.TryGetComponent<PlayerController>(out _))
            {
                OnPlayerTrigger.Invoke();
                StartCoroutine(Respawn(collision.gameObject));
            }
            else if(_respawnZombies && collision.gameObject.TryGetComponent<ZombieController>(out var zombie))
            {
                OnZombieTrigger.Invoke();
                StartCoroutine(RespawnZombie(zombie));
            }
        }

        private IEnumerator Respawn(GameObject player)
        {            
            yield return new WaitForSeconds(.5f);
            var rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            player.transform.position = _respawn.transform.position;
            StartCoroutine(BlinkCharacter(player));            
        }

        private IEnumerator RespawnZombie(ZombieController player)
        {
            yield return new WaitForSeconds(.5f);
            var rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            player.transform.position = player.Spawn;
        }

        public IEnumerator BlinkCharacter(GameObject player)
        {
            var sprites = player.GetComponentsInChildren<SpriteRenderer>();
            bool blink = false;
            float blinkTime = _blinkSecs;
            while (blinkTime > 0)
            {
                blink = !blink;
                if (blink)
                {
                    foreach (var sprite in sprites)
                    {
                        sprite.enabled = false;
                    }
                }
                else
                {
                    foreach (var sprite in sprites)
                    {
                        sprite.enabled = true;
                    }
                }
                blinkTime -= .1f;
                yield return new WaitForSeconds(.1f);
            }

            foreach (var sprite in sprites)
            {
                sprite.enabled = true;
            }
        }
    }
}