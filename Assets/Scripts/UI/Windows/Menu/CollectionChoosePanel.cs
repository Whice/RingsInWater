using Model;
using RingInWater.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RingInWater.UI
{
    public class CollectionChoosePanel : MonoBehaviourLogger
    {

        [Inject] private PlayerInfo playerInfo = null;
        private CollectionModel collectionModel
        {
            get => this.playerInfo.collectionModel;
        }
    }
}