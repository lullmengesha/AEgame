using Assets._Scripts;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public SimpleLoadingBar loadingBar;

    public static event EventHandler OnGameStarted;

    public void OnLoadingComplete() 
        {
        UIManager.Instance.TogglePanel("Loading");
        UIManager.Instance.TogglePanel("main");
        UIManager.Instance.TogglePanel("GamePlay");
        OnGameStarted?.Invoke(this, EventArgs.Empty);
    }
    public void OnNewGameClicked()
    {
        AudioManager.Instance.Play("click");
     
        loadingBar.FillBar(OnLoadingComplete);
    }
  public  void OnPauseBtnClicked()
    {
        {
            UIManager.Instance.TogglePanel("Pause");
        }
    }
    public void Resume()
    {
        {
            UIManager.Instance.TogglePanel("Pause");
        }
    }

}
