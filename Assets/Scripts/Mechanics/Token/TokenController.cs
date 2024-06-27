using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class animates all token instances in a scene.
    /// This allows a single update call to animate hundreds of sprite 
    /// animations.
    /// If the tokens property is empty, it will automatically find and load 
    /// all token instances in the scene at runtime.
    /// </summary>
    public class TokenController : MonoBehaviour, ITokenController
    {
        [Tooltip("Frames per second at which tokens are animated.")]
        [SerializeField]
        private float _frameRate = 12;
        [Tooltip("Instances of tokens which are animated. If empty, token instances are found and loaded at runtime.")]
        [SerializeField]
        private TokenInstance[] _tokens;
        [SerializeField] 
        private int _congratulationsTokenCount;

        private float _nextFrameTime = 0;
        private int _tokenCount;

        public bool IsCongratulationsTokenCount => _tokenCount == _congratulationsTokenCount;

        [ContextMenu("Find All Tokens")]
        private void FindAllTokensInScene()
        {
            _tokens = FindObjectsOfType<TokenInstance>();
        }

        private void Awake()
        {
            if (_tokens.Length == 0)
            {
                FindAllTokensInScene();
            }
            
            for (var i = 0; i < _tokens.Length; i++)
            {
                _tokens[i].tokenIndex = i;
                _tokens[i].OnColletedEvent += TokenCollected;
            }
        }

        private void Update()
        {
            //if it's time for the next frame...
            if (Time.time - _nextFrameTime > (1f / _frameRate))
            {
                //update all tokens with the next animation frame.
                for (var i = 0; i < _tokens.Length; i++)
                {
                    var token = _tokens[i];
                    //if token is null, it has been disabled and is no longer animated.
                    if (token != null)
                    {
                        token._renderer.sprite = token.sprites[token.frame];
                        if (token.collected && token.frame == token.sprites.Length - 1)
                        {
                            token.gameObject.SetActive(false);
                            _tokens[i] = null;
                        }
                        else
                        {
                            token.frame = (token.frame + 1) % token.sprites.Length;
                        }
                    }
                }
                //calculate the time of the next frame.
                _nextFrameTime += 1f / _frameRate;
            }
        }

        private void TokenCollected()
        {
            _tokenCount++;
        }

        public void Reset()
        {
            _tokenCount = 0;
        }

        private void OnDestroy()
        {
            foreach (TokenInstance token in _tokens)
            {
                if (!token)
                {
                    return;
                }
                token.OnColletedEvent -= TokenCollected;
            }
        }
    }
}