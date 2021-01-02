namespace PixelPlatformer
{
    using UnityEngine;

    public class ZombieController : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private float _movementSmoothing = .05f;
        private Vector3 _velocity = Vector3.zero;
        private bool _move = false;
        [SerializeField] private float _vision = 5f;
        [SerializeField] private float _runSpeed = 2f;
        private GameObject _spawnPoint;

        public Vector3 Spawn
        {
            get; set;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            Spawn = transform.position;
        }
        void OnDrawGizmosSelected()
        {
            // Draws a 5 unit long red line in front of the object
            Gizmos.color = Color.red;
            Vector3 direction = transform.TransformDirection(-1, 0, 0) * _vision;
            Gizmos.DrawRay(transform.position, direction);
        }

        private void FixedUpdate()
        {
            Vector2 direction = transform.TransformDirection(-1, 0, 0);
            var collisions = Physics2D.RaycastAll(transform.position, direction, _vision);
            foreach (var collision in collisions)
            {
                if (collision.collider != null && collision.collider.TryGetComponent<PlayerController>(out _))
                {
                    _move = true;
                    _animator.SetBool("IsRunning", true);
                }
                else
                {
                    _move = false;
                    _animator.SetBool("IsRunning", false);
                }

                if (_move)
                {
                    Move();
                }
            }
            
        }

        public void Move()
        {
            var move = (-1f * _runSpeed) * Time.fixedDeltaTime;
            
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, _rb.velocity.y);
            // And then smoothing it out and applying it to the character
            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        }
    }
}