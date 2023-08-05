using RingInWater.Utility;
using System;
using UnityEngine;

namespace RingInWater.UI
{
    public abstract class AbstractWindow : MonoBehaviourLogger
    {
        [SerializeField] private bool _isRememberForBack = true;

        /// <summary>
        /// Надо ли запоминать меню, чтобы потом к нему можно было вренуться.
        /// </summary>
        public bool isRememberForBack
        {
            get => _isRememberForBack;
        }
        protected WindowsController windowsController;
        /// <summary>
        /// Выполнить необходимые инициализации после создания окна.
        /// </summary>
        protected abstract void OnCreate();
        /// <summary>
        /// Выдать для окна его контроллер.
        /// <br/>После этого окно считаеся созданным и будет вызван метод <see cref="OnCreate"/>
        /// </summary>
        /// <param name="controller"></param>
        public void SetWindowsController(WindowsController controller)
        {
            windowsController = controller;
            OnCreate();
        }
        /// <summary>
        /// Передать контроллеру запрос на открытия другого меню.
        /// </summary>
        /// <param name="type"></param>
        protected void OpenWindow(Type type)
        {
            windowsController.OpenWindow(type);
        }

        /// <summary>
        /// Инициализировать окно данными при его активации.
        /// </summary>
        protected virtual void OnOpen() { }

        protected virtual void OnClose() { }
        /// <summary>
        /// Установить активность объекта меню, на котором висит скрипт.
        /// </summary>
        /// <param name="isActive"></param>
        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                OnOpen();
            }
            else
            {
                OnClose();
            }
            gameObject.SetActive(isActive);
        }

        /// <summary>
        /// Выполнить десйтвия во время уничтожения окна.
        /// </summary>
        protected abstract void OnWindowDestroy();

        private void OnDestroy()
        {
            OnWindowDestroy();
        }
    }
}