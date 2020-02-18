using UI;
using UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class Main : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
        }

        private void Start()
        {
            WindowController.Instance.Open<BottomBarWindow>();
        }
    }
}
