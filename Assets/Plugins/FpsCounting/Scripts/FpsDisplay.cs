//Copyright: Made by Appfox

using UnityEngine;

namespace WarpTravelAR.Utils.FPSCounting
{
    [RequireComponent(typeof(FpsCounter))]
    public class FpsDisplay : MonoBehaviour
    {
        [SerializeField] private int MaxFps = 999;
        [SerializeField] private bool _isEnablePFSInStartGame = true;

        [SerializeField] private OutputLine _averageLabel;
        [SerializeField] private OutputLine _highestLabel;
        [SerializeField] private OutputLine _lowestLabel;

        [SerializeField] private FpsConfigs _fpsConfigs;

        private FpsCounter _fpsCounter;

        private string[] stringNumbers;

        private void Awake()
        {
            _fpsCounter = GetComponent<FpsCounter>();

            Application.targetFrameRate = this.MaxFps;
            ++MaxFps;
            stringNumbers = new string[MaxFps];
            for (int i = 0; i < MaxFps; i++)
            {
                stringNumbers[i] = i.ToString();
            }
            UpdateIsFPSEnableFromPersistent();
            isFpsEnable = _isEnablePFSInStartGame;//Удалить строку, если фпс будет включаться из настроек, т.к. в этом случае включен фпс или нет, будет храниться в перфсах.
        }
        private void Update()
        {
            Display(_averageLabel, _fpsCounter.AverageFPS);
            Display(_highestLabel, _fpsCounter.HighestPFS);
            Display(_lowestLabel, _fpsCounter.LowersFPS);
        }

        private void Display(OutputLine label, int fps)
        {
            string value = stringNumbers[Mathf.Clamp(fps, 0, MaxFps - 1)];
            Color color = Color.white;
            for (int i = 0; i < _fpsConfigs.FPSData.Count; i++)
            {
                var fpsConfig = _fpsConfigs.FPSData[i];
                if (fps >= fpsConfig.MinFPS)
                {
                    color = fpsConfig.Color;
                    break;
                }
            }
            label.SetValueAndColor(value, color);
        }

        #region Saved data.


        private bool _isFpsEnable;
        public bool isFpsEnable
        {
            get => _isFpsEnable;
            set
            {
                PlayerPrefs.SetInt(nameof(_isFpsEnable), value ? 1 : 0);
                _isFpsEnable = value;
                gameObject.SetActive(value);
            }
        }
        private void UpdateIsFPSEnableFromPersistent()
        {
            isFpsEnable = PlayerPrefs.GetInt(nameof(_isFpsEnable), _isEnablePFSInStartGame ? 1 : 0) != 0;
        }

        #endregion Saved data.
    }
}