using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Exit);
        }
    }
}