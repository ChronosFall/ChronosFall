using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class OpenESCMenu : MonoBehaviour
{
    public Volume escMenuVolume;
    public bool isMenuActive = false;

    private DepthOfField _depthOfField;

    void Start()
    {
        escMenuVolume.profile.TryGet(out _depthOfField);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMenuState();
        }
    }

    void ChangeMenuState()
    {
        if (isMenuActive)
        {
            Debug.Log("Closing ESC Menu");
            isMenuActive = false;
            _depthOfField.focusMode.value = DepthOfFieldMode.Off;
        }
        else
        {
            // Logic to open the menu
            Debug.Log("Opening ESC Menu");
            isMenuActive = true;
            _depthOfField.focusMode.value = DepthOfFieldMode.Manual;
        }
    }
}