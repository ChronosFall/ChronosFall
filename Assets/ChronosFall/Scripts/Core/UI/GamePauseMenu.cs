using ChronosFall.Scripts.Configs;
using UnityEngine;
using UnityEngine.Rendering;

namespace ChronosFall.Scripts.Systems.UI
{
    public class GamePauseMenu : MonoBehaviour
    {
        [SerializeField] private Volume volume; // 背景をぼかすVolume

        private void Update()
        {
            if (Input.GetKeyDown(SystemKey.GamePauseMenu))
            {
                OpenGamePauseMenu();
            } 
        }
        
        private void OpenGamePauseMenu()
        {
            volume.enabled = volume.enabled ? false : true;
        }
    }
}