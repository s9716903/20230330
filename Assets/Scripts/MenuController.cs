using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaulVolume = 1;

    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Levels to Load")]
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Graphics Setting")]
    [SerializeField] private Slider Brightness = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdowm;
    private Resolution[] resolutions;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    //////
    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdowm.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndox = 0;

        for(int i = 0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndox = i;
            }
        }
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //////
    public void NewGameDialogYes()
    {
        GameManager.gameManager_instance.StartNewGame();
        LordingUI.NextScene = 5;
        SceneManager.LoadScene(2);
    }
    
    public void LoadGameDialogYes()
    {
        LordingUI.NextScene = 5;
        SceneManager.LoadScene(2);
        //if(PlayerPrefs.HasKey("SavedLevel"))
        //{
        //    levelToLoad = PlayerPrefs.GetString("SaveLevel");
        //    SceneManager.LoadScene(levelToLoad);
        //}
        //else
        // {
        //     noSavedGameDialog.SetActive(true);
        // }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void volumeApply ()
    {
        PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
        //StartCoroutine(CondirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            Brightness.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            graphicsApply();
        }

        if (MenuType == "Audio")
        {
            AudioListener.volume = defaulVolume;
            volumeSlider.value = defaulVolume;
            volumeTextValue.text = defaulVolume.ToString("0.0");
            volumeApply();
        }
    }

    /*public IEnumerator CondirmationBox() 
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }*/

///////
    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool fullscreen)
    {
        _isFullScreen = fullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void graphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        //StartCoroutine(CondirmationBox());
    }

////////////
}
