using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using Random = UnityEngine.Random;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the 
    /// TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : MonoBehaviour
    {
        public event Action OnColletedEvent;
        
        public AudioClip tokenCollectAudio;
        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime = false;
        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        internal Sprite[] sprites = new Sprite[0];

        internal SpriteRenderer _renderer;
        private AudioSource _audioSource;

        //unique index which is assigned by the TokenController in a scene.
        internal int tokenIndex = -1;
        //active frame in animation, updated by the controller.
        internal int frame = 0;
        internal bool collected = false;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            ITokenHandler tokenHandler = other.gameObject.GetComponent<ITokenHandler>();
            if (tokenHandler == null)
            {
                return;
            }
            OnPlayerEnter(tokenHandler);
        }

        void OnPlayerEnter(ITokenHandler tokenHandler)
        {
            if (collected)
            {
                return;
            }
            
            frame = 0;
            sprites = collectedAnimation;
            collected = true;
            tokenHandler.Collect();
            _audioSource.clip = tokenCollectAudio;
            _audioSource.Play();
            OnColletedEvent?.Invoke();
        }
    }
}