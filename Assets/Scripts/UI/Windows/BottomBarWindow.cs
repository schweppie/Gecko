using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class BottomBarWindow : Window
    {
        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private Button normalSpeedButton;

        [SerializeField]
        private Button fastForwardButton;

        [SerializeField]
        private Button topBarButton;

        private void Awake()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClick);
            normalSpeedButton.onClick.AddListener(OnNormalSpeedButtonClick);
            fastForwardButton.onClick.AddListener(OnFastForwardButtonClick);

            topBarButton.onClick.AddListener(OnTopBarButtonClick);
        }

        private void OnTopBarButtonClick()
        {
            WindowController.Instance.Open<TopBarWindow>();
        }

        private void OnFastForwardButtonClick()
        {
            Time.timeScale = 3;
        }

        private void OnNormalSpeedButtonClick()
        {
            Time.timeScale = 1;
        }

        private void OnPauseButtonClick()
        {
            Time.timeScale = 0;
        }

        public override int GetLayer()
        {
            return 1;
        }
    }
}
