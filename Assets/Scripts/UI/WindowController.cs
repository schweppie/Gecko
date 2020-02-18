using JP.Framework.Flow;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WindowController : SingletonBehaviour<WindowController>
    {
        private Dictionary<Type, Window> windows;
        public Dictionary<Type, Window> Windows => windows;

        private Dictionary<int, Window> activeWindows = new Dictionary<int, Window>();

        private void Awake()
        {
            windows = new Dictionary<Type, Window>();

            var windowObjects = GetComponentsInChildren<Window>(true);

            for (int i = 0; i < windowObjects.Length; i++)
            {
                Debug.Log("WindowController - Adding window " + windowObjects[i].ToString());
                windows.Add(windowObjects[i].GetType(), windowObjects[i]);
                windowObjects[i].gameObject.SetActive(false);
            }

            foreach (var window in windows.Values)
                (window as Window).Initialize();
        }

        public void Open<T>() where T : Window
        {
            if (!windows.ContainsKey(typeof(T)))
                return;

            Open((T)windows[typeof(T)]);
        }

        public void Open(Window window)
        {
            int windowLayer = window.GetLayer();

            if (activeWindows.ContainsKey(windowLayer))
                activeWindows[windowLayer].Hide();

            activeWindows[windowLayer] = window;

            window.Show();
        }

        public void Close<T>() where T : Window
        {
            if (!windows.ContainsKey(typeof(T)))
                return;

            Close((T)windows[typeof(T)]);
        }

        public void Close(Window window)
        {
            int windowLayer = window.GetLayer();

            if (activeWindows.ContainsKey(windowLayer))
                activeWindows[windowLayer].Hide();

            activeWindows.Remove(windowLayer);

            window.Hide();
        }
    }
}
