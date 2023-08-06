using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private LocalizedText useTmp = null;
        [SerializeField] private LocalizedText unuseTmp = null;
        [SerializeField] private Toggle toggle = null;

        #region Toggle group 

        /// <summary>
        /// Задать группу этой кнопке.
        /// </summary>
        /// <param name="group"></param>
        public void SetToggleGroup(ToggleGroup group)
        {
            toggle.group = group;
        }
        /// <summary>
        /// Задать группу для указанных кнопок.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="toggleButtons"></param>
        public static void SetToggleGroup(ToggleGroup group, params ToggleButton[] toggleButtons)
        {
            foreach (ToggleButton button in toggleButtons)
            {
                button.SetToggleGroup(group);
            }
        }

        #endregion Toggle group 

        #region Text

        public LocalizedText useText
        {
            get => useTmp;
        }
        public LocalizedText unuseText
        {
            get => unuseTmp;
        }

        #endregion Text

        #region Value

        /// <summary>
        /// Событие изменения состояния кнопки.
        /// </summary>
        public event UnityAction<bool> valueChanged;
        public bool isOn
        {
            get => toggle.isOn;
            set => toggle.isOn = value;
        }

        #endregion Value

        private void Awake()
        {
            toggle.onValueChanged.AddListener((call) => valueChanged?.Invoke(call));
        }
        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}