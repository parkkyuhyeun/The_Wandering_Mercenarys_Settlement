using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneUI : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;

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
}
