
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _loose;
    [SerializeField] private string _sceneName;


    public void ShowWinCanvas()
    {
        _win.SetActive(true);
    }

    public void ShowLooseCanvas()
    {
        _loose.SetActive(true);
    }

    public void StartAgain()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
