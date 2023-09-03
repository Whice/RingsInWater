using System;
using UnityEngine;

namespace RingInWater.Utility
{
    /// <summary>
    /// Создан для упрощения выдачи сообщений в консоль,
    /// а также формирования своих.
    /// </summary>
    public class MonoBehaviourLogger : InjectableMonoBehaviour, ILogger
    {
        /// <summary>
        /// Вывести сообщение об ошибке в консоль.
        /// </summary>
        /// <param name="message"></param>
        public void LogError(String message)
        {
            Debug.LogError(message);
        }
        /// <summary>
        /// Вывести сообщение об ошибке в консоль.
        /// </summary>
        /// <param name="message"></param>
        public void LogError(object message)
        {
            Debug.LogError(message);
        }
        /// <summary>
        /// Вывести предупреждене в консоль.
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(String message)
        {
            Debug.LogWarning(message);
        }
        /// <summary>
        /// Вывести предупреждене в консоль.
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }
        /// <summary>
        /// Вывести сообщение в консоль.
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(String message)
        {
            Debug.Log(message);
        }
        /// <summary>
        /// Вывести сообщение в консоль.
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(object message)
        {
            Debug.Log(message);
        }

        /// <summary>
        /// Проверить ссылку на объект на присутсвие.
        /// <br/>Если ссылка нулевая, то выведется ошибка с именем объекта, которое было указано.
        /// </summary>
        /// <param name="checkableObject"></param>
        /// <param name="objectName"></param>
        /// <param name="isError">По умолчания - ошибка. Установить false, если не надо выводить ошибку в консоль.</param>
        /// <returns>Если null, то true.</returns>
        public bool IsNullCheck(object checkableObject, string objectName, bool isError = true)
        {
            if (isError && checkableObject == null)
            {
                LogError($"{objectName} is null!");
            }

            return checkableObject == null;
        }
    }
}