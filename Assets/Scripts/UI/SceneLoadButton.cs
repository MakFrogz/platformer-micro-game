using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class SceneLoadButton : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Load);
        }

        private void Load()
        {
            SceneManager.LoadScene(_sceneName);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Load);
        }
    }
}