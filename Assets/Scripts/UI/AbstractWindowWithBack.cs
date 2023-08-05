using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    /// <summary>
    /// Окно имеющее кнопку, для возвращения на предыдущее окно.
    /// </summary>
    public abstract class AbstractWindowWithBack : AbstractWindow
    {
        /// <summary>
        /// Кнопка для возвращения на предыдущее окно.
        /// </summary>
        [SerializeField] private Button backButton = null;
        /// <summary>
        /// Обработать события возвращения к предыдущему меню.
        /// </summary>
        protected virtual void OnBack()
        {
            windowsController.OpenPreviousWindow();
        }
        protected override void OnCreate()
        {
            if (backButton == null)
            {
                LogError($"Back button is not set in {gameObject.name}");
            }
            backButton.onClick.AddListener(OnBack);
        }
        protected override void OnOpen() { }
        protected override void OnWindowDestroy()
        {
            backButton.onClick.RemoveAllListeners();
        }
    }
}