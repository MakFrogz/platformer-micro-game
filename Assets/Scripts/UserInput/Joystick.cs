using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInput
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IInputReader, IDirectionReader
    {
        public event Action<Vector2> OnMove;
        public event Action OnStop;

        [Header("General")]
        [SerializeField] 
        private RectTransform _stickContainerTransform;
        
        [SerializeField] 
        private RectTransform _stickTransform;

        [SerializeField] 
        private Vector2 defaultPosition;

        [Header("Stick settings")]
        [SerializeField] 
        private float _dragMovementDistance;
    
        [SerializeField] 
        private float _dragOffsetDistance;

        private Vector2 _direction;
        public Vector2 Direction => _direction.normalized;

        private void Start()
        {
            _stickContainerTransform.anchoredPosition = defaultPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(_stickTransform, eventData.position, eventData.pressEventCamera, out Vector2 offset);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_stickTransform, eventData.position, eventData.pressEventCamera, out _direction);
            _direction = Vector2.ClampMagnitude(_direction, _dragOffsetDistance) / _dragOffsetDistance;
            _stickTransform.anchoredPosition = _direction * _dragMovementDistance;
            OnMove?.Invoke(_direction.normalized);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _stickContainerTransform.anchoredPosition = defaultPosition;
            _stickTransform.anchoredPosition = Vector2.zero;
            _direction = Vector2.zero;
            OnStop?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _stickContainerTransform.position = eventData.position;
        }
    }
}
