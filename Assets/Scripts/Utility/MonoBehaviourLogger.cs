using System;
using UnityEngine;

namespace RingInWater.Utility
{
    /// <summary>
    /// Создан для упрощения выдачи сообщений в консоль,
    /// а также формирования своих.
    /// </summary>
    public class MonoBehaviourLogger : InjectableMonoBehaviour
    {
        /// <summary>
        /// Вывести сообщение об ошибке в консоль.
        /// </summary>
        /// <param name="message"></param>
        protected void LogError(String message)
        {
            Debug.LogError(message);
        }
        /// <summary>
        /// Вывести сообщение об ошибке в консоль.
        /// </summary>
        /// <param name="message"></param>
        protected void LogError(object message)
        {
            Debug.LogError(message);
        }
        /// <summary>
        /// Вывести предупреждене в консоль.
        /// </summary>
        /// <param name="message"></param>
        protected void LogWarning(String message)
        {
            Debug.LogWarning(message);
        }
        /// <summary>
        /// Вывести предупреждене в консоль.
        /// </summary>
        /// <param name="message"></param>
        protected void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }
        /// <summary>
        /// Вывести сообщение в консоль.
        /// </summary>
        /// <param name="message"></param>
        protected void LogInfo(String message)
        {
            Debug.Log(message);
        }
        /// <summary>
        /// Вывести сообщение в консоль.
        /// </summary>
        /// <param name="message"></param>
        protected void LogInfo(object message)
        {
            Debug.Log(message);
        }
    }
}