using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        private PatrolPath.Mover mover;
        private AnimationController control;
        private Collider2D _collider;
        private AudioSource _audio;
        private SpriteRenderer spriteRenderer;
        private Health _health;

        public Bounds Bounds => _collider.bounds;

        private void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _health = GetComponent<Health>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ICollision player = collision.gameObject.GetComponent<ICollision>();
            if (player == null)
            {
                return;
            }

            var willHurtEnemy = player.Bounds.center.y >= Bounds.max.y;

            if (willHurtEnemy)
            {
                if (_health != null)
                {
                    _health.Decrement();
                    if (!_health.IsAlive)
                    {
                        Death();
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Death();
                    player.Bounce(2);
                }
            }
            else
            {
                IHealthHandler healthHandler = collision.gameObject.GetComponent<IHealthHandler>();
                healthHandler.Damage();
            }
        }

        private void Death()
        {
            _collider.enabled = false;
            control.enabled = false;
            if (_audio && ouch)
                _audio.PlayOneShot(ouch);
        }

        private void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}