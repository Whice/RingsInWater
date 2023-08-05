using RingInWater.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.UI
{
    public class WindowsController : MonoBehaviourLogger
    {
        [SerializeField] private int startWindowIndex;
        [SerializeField] private AbstractWindow[] windows = new AbstractWindow[0];

        private Dictionary<Type, AbstractWindow> windowsDic;
        private Stack<AbstractWindow> openedWindows = new Stack<AbstractWindow>();

        private void InitWindows()
        {
            windowsDic = new Dictionary<Type, AbstractWindow>(windows.Length);

            foreach (AbstractWindow window in windows)
            {
                window.SetWindowsController(this);
                windowsDic.Add(window.GetType(), window);
                window.SetActive(false);
            }

            currentWindow = windows[0];
            OpenWindow(windows[startWindowIndex].GetType());
        }

        public AbstractWindow currentWindow { get; private set; }

        public event Action windowChanged;

        private void OnWindowChanged()
        {
            LogInfo($"Current window: {currentWindow.GetType()}");
            windowChanged?.Invoke();
        }

        /// <summary>
        /// Открыть все окна, кроме текущего. Текущее окно будет включено.
        /// </summary>
        private void HideAllWindowsAndActiveCurrent()
        {
            foreach (AbstractWindow menu in windows)
            {
                menu.SetActive(menu == currentWindow);
            }

            OnWindowChanged();
        }

        public T GetWindow<T>() where T : AbstractWindow
        {
            if (windowsDic.TryGetValue(typeof(T), out var window))
            {
                return window as T;
            }

            throw new Exception("Window with type {windowType} is not found!");
        }

        /// <summary>
        /// Получить окно из списка, если такое есть.
        /// </summary>
        /// <param name="windowType"></param>
        /// <returns>null, если окно не было найдено.</returns>
        public AbstractWindow GetWindowByType(Type windowType)
        {
            if (windowsDic.TryGetValue(windowType, out var window))
            {
                return window;
            }

            throw new Exception("Window with type {windowType} is not found!");
        }

        public void OpenWindow<T>() where T : AbstractWindow
        {
            OpenWindow(typeof(T));
        }

        /// <summary>
        /// Открыть меню заданного типа.
        /// <br/>Все меню прочих типов будут скрыты.
        /// <br/>Текущее меню будет сохранено для возвращения обратно,
        /// если в нем указано, что это можно сделать.
        /// </summary>
        /// 
        public void OpenWindow(Type type)
        {
            AbstractWindow foundWindow = GetWindowByType(type);

            if (foundWindow != null)
            {
                if (currentWindow != null && currentWindow.isRememberForBack)
                {
                    openedWindows.Push(currentWindow);
                }

                currentWindow = foundWindow;
                HideAllWindowsAndActiveCurrent();
            }
        }

        /// <summary>
        /// Открыть окно, которое было открыто перед нынешним.
        /// </summary>
        public bool OpenPreviousWindow()
        {
            bool isHavePrevious = openedWindows.Count > 0;
            if (isHavePrevious)
            {
                currentWindow = openedWindows.Pop();
                HideAllWindowsAndActiveCurrent();
            }

            return isHavePrevious;
        }

        private void Awake()
        {
            InitWindows();
        }
    }
}