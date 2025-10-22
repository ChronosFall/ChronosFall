using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace ChronosFall.Scripts.Characters.PlayerControl
{
    public class OpenEscMenu : MonoBehaviour
    {
        public Volume escMenuVolume;
        public bool isMenuActive = false;

        private DepthOfField _depthOfField;

        private void Start()
        {
            escMenuVolume.profile.TryGet(out _depthOfField);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeMenuState();
            }
        }

        /// <summary>
        /// メニュー切り替え
        /// </summary>
        private void ChangeMenuState()
        {
            if (isMenuActive)
            {
                Debug.Log("Closing ESC Menu");
                isMenuActive = false;
                _depthOfField.focusMode.value = DepthOfFieldMode.Off;
            }
            else
            {
                Debug.Log("Opening ESC Menu");
                isMenuActive = true;
                _depthOfField.focusMode.value = DepthOfFieldMode.Manual;
            }
        }
    }
}