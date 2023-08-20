using UnityEngine;

namespace RingInWater.Utility
{
    /// <summary>
    /// Допольнительные возможности для наследников <see cref="MonoBehaviour"/>
    /// </summary>
    public class MonoBehaviourAdditionals : MonoBehaviour
    {
        /// <summary>
        /// Установить активность для <see cref="GameObject"/> этого компонента.
        /// </summary>
        public void SetActiveObject(bool isActive)
        {
            this.gameObject.SetActive(isActive);
        }
    }
}