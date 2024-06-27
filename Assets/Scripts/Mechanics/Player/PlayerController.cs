using System;
using System.Collections.Generic;
using Boosts;
using DefaultNamespace;
using Platformer.Mechanics;
using UnityEngine;

namespace Mechanics.Player
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject, IBoostApply, IPlayerMovement, ICollision
    {
        public event Action OnDamageEvent;
        public event Action OnDieEvent;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public PlatformerSettings PlatformerSettings;


        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;

        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health.Health health;
        public bool controlEnabled = true;

        private bool _jump;
        private Vector2 _move;
        private SpriteRenderer _spriteRenderer;
        private Dictionary<AbilityType, float> _multipliers = new Dictionary<AbilityType, float>();
        
        public Animator Animator { get; private set; }
        public Bounds Bounds => collider2d.bounds;
        public Vector2 Velocity => velocity;

        void Awake()
        {
            health = GetComponent<Health.Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                if (jumpState == JumpState.Grounded && _move.y > 0)
                    jumpState = JumpState.PrepareToJump;
                else if (_move.y <= 0)
                {
                    stopJump = true;
                }
            }
            else
            {
                _move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            _jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    _jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (_jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * GetMultiplier(AbilityType.Jump) * PlatformerSettings.JumpModifier;
                _jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * PlatformerSettings.JumpDeceleration;
                }
            }

            if (_move.x > 0.01f)
                _spriteRenderer.flipX = false;
            else if (_move.x < -0.01f)
                _spriteRenderer.flipX = true;

            Animator.SetBool("grounded", IsGrounded);
            Animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = _move * (maxSpeed * GetMultiplier(AbilityType.Speed));
        }

        public void ApplyBoost(AbilityType abilityType, float multiplier)
        {
            if (_multipliers.ContainsKey(abilityType))
            {
                _multipliers[abilityType] = multiplier;
                return;
            }
            
            _multipliers.Add(abilityType, multiplier);
        }

        private float GetMultiplier(AbilityType abilityType)
        {
            return _multipliers.TryGetValue(abilityType, out float multiplier) ? multiplier : 1f;
        }

        public void ControlEnable()
        {
            controlEnabled = true;
        }

        public void ControlDisable()
        {
            controlEnabled = false;
        }

        public void SetDirection(Vector2 direction)
        {
            _move = direction;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}