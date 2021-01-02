namespace PixelPlatformer
{
    using System.Collections;
    using UnityEngine;

    public class DeathZone : MonoBehaviour
    {
        [SerializeField]
        private GameObject _respawn;
        [SerializeField]
        private float _blinkSecs = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out _))
            {
                StartCoroutine(Respawn(collision.gameObject));
            }
            if(collision.gameObject.TryGetComponent<ZombieController>(out var zombie))
            {
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