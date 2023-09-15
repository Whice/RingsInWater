using Model;
using RingInWater.Utility;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    /// <summary>
    /// Компонент для отображения выбираемой сущности.
    /// </summary>
    public class CollectionEntityVisualizator : MonoBehaviourLogger
    {
        [SerializeField] private TextMeshProUGUI nameTextField = null;
        [SerializeField] private Image image = null;
        [SerializeField] private Button button = null;

        private CollectionEntity entity;
        /// <summary>
        /// Задать данные для отображения сущности.
        /// Каждый раз при инициализации проводит проверку данных на нуевые значения.
        /// </summary>
        /// <param name="entity"></param>
        public void Initialize(CollectionEntity entity)
        {
            IsNullCheck(this.nameTextField, nameof(this.nameTextField));
            IsNullCheck(this.image, nameof(this.image));
            IsNullCheck(this.button, nameof(this.button));
            if (entity == null)
            {
                LogError("Enity is not set!");
                return;
            }

            if(entity.icon == null)
            {
                this.image.color = entity.color;
            }
            else
            {
                this.image.sprite = entity.icon;
            }
            this.entity = entity;
            this.nameTextField.text = entity.name;

            this.button.onClick.RemoveAllListeners();
            this.button.onClick.AddListener(()=>this.entityChossed?.Invoke(entity));
        }
        /// <summary>
        /// Сущность была выбрана.
        /// </summary>
        public event Action<CollectionEntity> entityChossed;
    }
}