using UnityEngine;

namespace Gameplay.Utility
{
    public class SpaceToSpeedUpTime : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Time.timeScale = 3;
            if (Input.GetKeyUp(KeyCode.Space))
                Time.timeScale = 1;
        }
#endif
    }
}
