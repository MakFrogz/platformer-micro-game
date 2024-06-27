using Model;
using UnityEngine;
using VContainer;

namespace Platformer.Mechanics
{
    public class TokenHandler : MonoBehaviour, ITokenHandler
    {
        private ITokenModel _tokenModel;

        [Inject]
        private void Construct(ITokenModel tokenModel)
        {
            _tokenModel = tokenModel;
        }

        public void Collect()
        {
            _tokenModel.AddToken();
        }
    }
}