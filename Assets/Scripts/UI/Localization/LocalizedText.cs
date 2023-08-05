using RingInWater.Utility;
using TMPro;
using UnityEngine;
using Zenject;

namespace RingInWater.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviourLogger
    {
        [SerializeField] private string key = string.Empty;
        [SerializeField] private bool isLocalized = true;

        [Inject] private LocalizaiotnKeeper keeper;

        private TextMeshProUGUI textField;

        private void OnTextSetForLangauge()
        {
            string text = keeper.GetLocalization(key);
            if(!string.IsNullOrEmpty(text))
            {
                textField.text = text;
                textField.font = keeper.currentFont;
            }
        }
        /// <summary>
        /// Установить ключ для текста и обновить текст.
        /// </summary>
        /// <param name="key">Ключ, указанный для локализации.</param>
        public void SetTextKey(string key)
        {
            this.key = key;
            OnTextSetForLangauge();
        }
        /// <summary>
        /// Установить текст напрямую, без ключа.
        /// Шрифт при этом останется такой, какой был указан ранее.
        /// </summary>
        public void SetText(string text)
        {
            textField.text = text;
        }
        private void Awake()
        {
            if (isLocalized)
            {
                textField = GetComponent<TextMeshProUGUI>();
                OnTextSetForLangauge();
                keeper.languageChanged += OnTextSetForLangauge;
            }
        }
        private void OnDestroy()
        {
            if (isLocalized)
                keeper.languageChanged -= OnTextSetForLangauge;

        }
    }
}