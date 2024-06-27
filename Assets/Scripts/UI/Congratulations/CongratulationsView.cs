using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Congratulations
{
    public class CongratulationsView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _label;
        [SerializeField ]private float _fadeInDuration = 1.5f;
        [SerializeField] private float _fadeInValue = 1f;
        [SerializeField] private float _scale;

        private Sequence _sequence;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence
                .Append(_canvasGroup.DOFade(_fadeInValue, _fadeInDuration))
                .Join(_label.gameObject.transform.DOScale(_scale, _fadeInDuration));
        }
    }
}