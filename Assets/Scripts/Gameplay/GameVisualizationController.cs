using System.Collections;
using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay
{
    public class GameVisualizationController : SingletonBehaviour<GameVisualizationController>
    {
        public delegate void VisualizeDelegate(int step, float t);
        public event VisualizeDelegate OnGameVisualization;

        public delegate void VoidDelegate();
        public event VoidDelegate OnVisualizationStart;
        public event VoidDelegate OnVisualizationComplete;

        public void DoVisualization()
        {
            OnVisualizationStart?.Invoke();

            StartCoroutine(VisualizationEnumerator());
        }

        private IEnumerator VisualizationEnumerator()
        {
            float t = 0f;

            while (t <= 1f)
            {
                t += Time.deltaTime * 2f;

                OnGameVisualization?.Invoke(GameStepController.Instance.Step, t);

                yield return null;
            }

            OnVisualizationComplete?.Invoke();
        }
    }
}
