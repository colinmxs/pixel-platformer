namespace PixelPlatformer
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 400f;              // Amount of force added when the player jumps.
        [Range(0, 100)] [SerializeField] private float _runSpeed = 2f;
        [Range(0, 2)] [SerializeField] private float _sprintSpeed = 2f;      // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private bool _airControl = false;             // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask _whatIsGround;              // A mask determining what is ground to the character
        [SerializeField] private Transform _groundCheck;             // A position marking where to check if the player is grounded.

        const float _groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool _isGrounded;            // Whether or not the player is grounded.
        private Rigidbody2D _rigidBody2d;
        private bool _isFlipped = false;  // For determining which way the player is currently facing.
        private bool _isAirborn = false;
        private bool _isIdle = false;
        private Vector3 _velocity = Vector3.zero;
        private bool _jump = false;
        private bool _sprint = false;
        private float _horizontalMove;
        
        [Header("Events")]
        [Space]

        public AnimationBoolEvent OnAnimationEvent;

        [System.Serializable]
        public class AnimationBoolEvent : UnityEvent<string, bool> { }

        [Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        private void Awake()
        {
            _rigidBody2d = GetComponent<Rigidbody2D>();

            if (OnAnimationEvent == null)
                OnAnimationEvent = new AnimationBoolEvent();
        }

        private void Update()
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
            
            if (Input.GetButtonDown("Jump")) _jump = true;

            //if (Input.GetAxisRaw("Sprint") > 0 || Input.GetButtonDown("Sprint")) _sprint = true;
        }

        private void FixedUpdate()
        {
            Move();
            _jump = false;
            bool isGrounded = false;

            _isAirborn = _rigidBody2d.velocity.y != 0 && !_isGrounded;
            _isIdle = Math.Round(_rigidBody2d.velocity.y, 1) == 0.0 && Math.Round(_rigidBody2d.velocity.x, 1) == 0.0;

            bool isRunning = false;
            bool isJumping = false;

            if (!_isIdle && !_isAirborn) isRunning = true;
            else if (!_isIdle && _isAirborn) isJumping = true;

            Collider2D[] groundColliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
            for (int i = 0; i < groundColliders.Length; i++)
            {
                if (groundColliders[i].gameObject != gameObject)
                {
                    isGrounded = true;
                }
            }

            _isGrounded = isGrounded;

            OnAnimationEvent.Invoke("IsAirborn", isJumping);
            OnAnimationEvent.Invoke("IsRunning", isRunning);
        }

        public void Move()
        {
            var move = _horizontalMove * Time.fixedDeltaTime;
            //only control the player if grounded or airControl is turned on
            if (_isGrounded || _airControl)
            {
                if (_sprint)
                {
                    move *= _sprintSpeed;
                }

                if (_isGrounded)
                {
                    // Move the character by finding the target velocity
                    Vector3 targetVelocity = new Vector2(move * 10f, _rigidBody2d.velocity.y);
                    // And then smoothing it out and applying it to the character
                    _rigidBody2d.velocity = Vector3.SmoothDamp(_rigidBody2d.velocity, targetVelocity, ref _velocity, _movementSmoothing);

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && _isFlipped)
                    {
                        // ... flip the player.
                        Flip();
                    }
                    // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && !_isFlipped)
                    {
                        // ... flip the player.
                        Flip();
                    }
                }
                else
                {
                    var targetVelocity = new Vector2(0f, _rigidBody2d.velocity.y / 2);
                    _rigidBody2d.velocity = Vector3.SmoothDamp(_rigidBody2d.velocity, targetVelocity, ref _velocity, _movementSmoothing);
                }
            }

            // If the player should jump...
            if (_isGrounded && _jump)
            {
                // Add a vertical force to the player.
                _rigidBody2d.AddForce(new Vector2(0f, _jumpForce));
            }

            //
        }
        
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            _isFlipped = !_isFlipped;
            OnAnimationEvent.Invoke("IsFlipped", _isFlipped);
        }
    }
}