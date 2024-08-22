using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneUI : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] Slider Hp_Slider;
    [SerializeField] Slider Pw_Slider;
    [SerializeField] TextMeshProUGUI HP_lostPointTxt;
    [SerializeField] TextMeshProUGUI PW_lostPointTxt;
    [SerializeField] TextMeshProUGUI HP_LvlTxt;
    [SerializeField] TextMeshProUGUI PW_LvlTxt;
    [SerializeField] TextMeshProUGUI R_PointTxt;

    SavePInf saveData;

    public int HLevel;
    public int PLevel;
    int HLostP = 1;
    int PLostP = 1;

    private void Awake()
    {
        Hp_Slider.maxValue = 10;
        Pw_Slider.maxValue = 10;
    }

    private void Update()
    {
        R_PointTxt.text = $"{saveData.R_Point} Point";

        HP_LvlTxt.text = $"Level. {HLevel}";
        PW_LvlTxt.text = $"Level. {PLevel}";

        HP_lostPointTxt.text = $"HP +1%\n-{HLostP} Point";
        PW_lostPointTxt.text = $"Damage +1%\n-{PLostP} Point";
    }

    public void StartBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenPanel()
    {
        upgradePanel.SetActive(true);
    }
    public void ClosePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HLevelUp()
    {
        HLevel++;
        Hp_Slider.value = HLevel;
        saveData.R_Point -= HLostP;
        saveData.H_UpLvl = HLevel;
        HLostP++;
        JsonManager<SavePInf>.SaveJson(saveData, "PlayerInformation");
    }

    public void PLevelUp()
    {
        PLevel++;
        Pw_Slider.value = PLevel;
        saveData.R_Point -= PLostP;
        saveData.P_UpLvl = PLevel;
        PLostP++;
        JsonManager<SavePInf>.SaveJson(saveData, "PlayerInformation");
    }
}
