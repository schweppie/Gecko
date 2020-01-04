using System.Collections;
using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay
{
    public class GameVisualizationController : SingletonBehaviour<GameVisualizationController>
    {
        public delegate void VisualizeDelegate(int step, float t);
        public event VisualizeDelegate OnGameVisualization;

        public delegate void voidDelegate();
        public event voidDelegate OnVisualizationStart;
        public event voidDelegate OnVisualizationComplete;

        public void DoVisualization()
        {
            if (OnVisualizationStart != null)
                OnVisualizationStart();

            StartCoroutine(VisualizationEnumerator());
        }

        private IEnumerator VisualizationEnumerator()
        {
            float t = 0f;

            while (t <= 1f)
            {
                t += Time.deltaTime;

                if (OnGameVisualization != null)
                    OnGameVisualization(GameStepController.Instance.Step, t);
                
                yield return null;
            }
            
            if (OnVisualizationComplete != null)
                OnVisualizationComplete();
        }
    }
}
