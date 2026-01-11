using ChronosFall.Scripts.Configs;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace ChronosFall.Scripts.Systems.UI
{
    public class GamePauseMenu
    {
        [SerializeField] private Volume _volume;
        private DepthOfField _depthOfField;

        private void Start()
        {
            _volume.profile.TryGet(out _depthOfField);
        }

        private void Update()
        {
            if (Input.GetKeyDown(SystemKey.GamePauseMenu))
            {
                OpenGamePauseMenu();
            } 
        }
        
        private void OpenGamePauseMenu()
        {
            if (_volume.enabled)
            {
                _volume.enabled = false;
            }
            else
            {
                _volume.enabled = true;
            }
        }
    }
}