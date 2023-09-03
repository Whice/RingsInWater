using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// Вся информация о игроке.
    /// </summary>
    [Serializable]
    public class PlayerInfo
    {
        /// <summary>
        /// Надо передать объект логгера, если нужно будет логирование.
        /// </summary>
        [NonSerialized] public RingInWater.Utility.ILogger logger;

        #region Ид представления колец и шпилей.

        /// <summary>
        /// Текущее представление колец.
        /// </summary>
        private RingViewId currentRingViewIdPrivate  = RingViewId.unknown;
        /// <summary>
        /// Текущее представление колец.
        /// </summary>
        public RingViewId currentRingViewId
        {
            get => this.currentRingViewIdPrivate;
            set
            {
                if (this.ringTypesAvailableToPlayer.Contains((int)value))
                {
                    this.currentRingViewIdPrivate = value;
                    //Save(this);
                }
                else
                {
                    this.logger?.LogError("Ring id was not found!");
                }
            }
        }
        /// <summary>
        /// Текущее представление шпилей.
        /// </summary>
        private SpiresViewId currentSpiresViewIdprivate = SpiresViewId.unknown;
        /// <summary>
        /// Текущее представление шпилей.
        /// </summary>
        public SpiresViewId currentSpiresViewId
        {
            get => this.currentSpiresViewIdprivate;
            set
            {
                if (this.spiresTypesAvailableToPlayer.Contains((int)value))
                {
                    this.currentSpiresViewIdprivate = value;
                    //Save(this);
                }
                else
                {
                    this.logger?.LogError("Spire id was not found!");
                }
            }
        }

        /// <summary>
        /// Доступные игроку виды колец.
        /// </summary>
        private HashSet<int> ringTypesAvailableToPlayer  = new HashSet<int>();
        /// <summary>
        /// Указанный ID доступен.
        /// </summary>
        public bool IsRingAvailable(RingViewId ringViewId)
        {
            return this.ringTypesAvailableToPlayer.Contains((int)ringViewId);
        }
        /// <summary>
        /// Доступные игроку виды колец.
        /// </summary>
        private HashSet<int> spiresTypesAvailableToPlayer = new HashSet<int>();
        /// <summary>
        /// Указанный ID доступен.
        /// </summary>
        public bool IsSpireAvailable(SpiresViewId spiresViewId)
        {
            return this.spiresTypesAvailableToPlayer.Contains((int)spiresViewId);
        }

        /// <summary>
        /// Добавить новый ид в список доступных игроку представлений.
        /// </summary>
        /// <param name="id"></param>
        public void AddRingIdToAvailable(RingViewId id)
        {
            int idInt = (int)id;
            if (this.ringTypesAvailableToPlayer.Contains(idInt))
            {
                this.ringTypesAvailableToPlayer.Add(idInt);
            }
            else
            {
                this.logger?.LogWarning($"ID {id} is already exist in list!");
            }
            //Save(this);
        }
        /// <summary>
        /// Добавить новый ид в список доступных игроку представлений.
        /// </summary>
        /// <param name="id"></param>
        public void AddSpireIdToAvailable(SpiresViewId id)
        {
            int idInt = (int)id;
            if (this.spiresTypesAvailableToPlayer.Contains(idInt))
            {
                this.spiresTypesAvailableToPlayer.Add(idInt);
            }
            else
            {
                this.logger?.LogWarning($"ID {id} is already exist in list!");
            }
            //Save(this);
        }


        #endregion Ид представления колец и шпилей.

        public CollectionModel collectionModel { get; private set; }

        private void OnCollectionEntityChooseChanged(CollectionEntityType type, int id)
        {
            switch(type)
            {
                case CollectionEntityType.spire:
                    {
                        currentSpiresViewId = (SpiresViewId)id;
                        break;
                    }
                case CollectionEntityType.ring:
                    {
                        currentRingViewId = (RingViewId)id;
                        break;
                    }
                case CollectionEntityType.bubble:
                    {
                        break;
                    }
            }
        }

        private void Initilize()
        {
            this.collectionModel = new CollectionModel();
            this.collectionModel.collectionEntityChooseChanged += OnCollectionEntityChooseChanged;
        }

        public PlayerInfo()
        {
            this.currentRingViewIdPrivate = RingViewId.test;
            this.currentSpiresViewIdprivate = SpiresViewId.test;
            Initilize();
        }

        #region Save/Load

        private const string SAVE_FILE_NAME = "playerSave.dft";
        public static void Save(PlayerInfo playerInfo)
        {
            string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            using (FileStream stream = File.OpenWrite(savePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, playerInfo);
            }
        }
        public static PlayerInfo Load()
        {
            string savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            if (File.Exists(savePath))
            {
                using (FileStream stream = File.OpenRead(savePath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    PlayerInfo info = (PlayerInfo)formatter.Deserialize(stream);
                    info.Initilize();
                    return info;
                }
            }
            else
            {
                return new PlayerInfo();
            }
        }

        #endregion Save/Load
    }
}
