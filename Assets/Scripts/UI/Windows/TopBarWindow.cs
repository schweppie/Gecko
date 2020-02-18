using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class TopBarWindow : Window
    {
        [SerializeField]
        private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            WindowController.Instance.Close(this);
        }

        public override int GetLayer()
        {
            return 0;
        }
    }
}
