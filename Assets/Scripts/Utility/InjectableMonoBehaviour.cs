using UnityEngine;
using Zenject;

namespace RingInWater.Utility
{
    /// <summary>
    /// Объект для инстанцирования со специальными методами
    /// для zenject.
    /// </summary>
    public class InjectableMonoBehaviour : MonoBehaviour
    {
        [Inject] private DiContainer diContainer;
        /// <summary>
        /// Создать объект с добавлением контейнера для внедерения зависимостей.
        /// </summary>
        protected T InstantiateWithInject<T>(T objectTemplate) where T : Object
        {
            return diContainer.InstantiatePrefabForComponent<T>(objectTemplate);
        }
        /// <summary>
        /// Создать объект с добавлением контейнера для внедерения зависимостей.
        /// </summary>
        protected T InstantiateWithInject<T>(T objectTemplate, Transform parent) where T : Object
        {
            return diContainer.InstantiatePrefabForComponent<T>(objectTemplate, parent);
        }
        /// <summary>
        /// Создать объект с добавлением контейнера для внедерения зависимостей.
        /// </summary>
        protected GameObject InstantiateWithInject(GameObject objectTemplate)
        {
            return diContainer.InstantiatePrefab(objectTemplate);
        }
    }
}