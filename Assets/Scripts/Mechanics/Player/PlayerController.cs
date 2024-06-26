using System;
using System.Collections;
using System.Collections.Generic;
using Boosts;
using UnityEngine;
using Platformer.Model;
using Platformer.Core;
using Platformer.UI.PlayerStats;
using VContainer;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject, IBoostApply, IDirectionApply, ICollision
    {
        public event Action OnDamageEvent;
        public event Action OnDieEvent;

        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;


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
        public Health health;
        public bool controlEnabled = true;

        private bool _jump;
        private Vector2 _move;
        private SpriteRenderer _spriteRenderer;


        //TODO refactor
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private Dictionary<AbilityType, float> _multipliers = new Dictionary<AbilityType, float>();
        public Animator Animator { get; private set; }

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
        }

        /*private void Move(Vector2 direction)
        {
            if (controlEnabled)
            {
                move.x = direction.x;
                if (jumpState == JumpState.Grounded && direction.y > 0f)
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (direction.y <= 0f)
                {
                    stopJump = true;
                    //Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
        }

        private void Stop()
        {
            Move(Vector2.zero);
        }*/


        /*protected override void Update()
        {
            if (controlEnabled)
            {
                _move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                _move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }*/


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
                        //Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        //Schedule<PlayerLanded>().player = this;
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
                velocity.y = jumpTakeOffSpeed * GetMultiplier(AbilityType.Jump) * model.jumpModifier;
                _jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
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

        public void ApplyDirection(Vector2 direction)
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