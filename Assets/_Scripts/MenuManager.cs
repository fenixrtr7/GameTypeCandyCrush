using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Activar MENU
    public void ShowMainMenu()
    {
        GameManager.sharedInstance.StopGame();
        menuCanvas.enabled = true;
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }
    // Activar "in GAME"
    public void ShowInGame()
    {
        GameManager.sharedInstance.InGame();
        menuCanvas.enabled = false;
        inGameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }
    // Activar "Game over"
    public void ShowGameOver()
    {
        GameManager.sharedInstance.StopGame();
        menuCanvas.enabled = false;
        inGameCanvas.enabled = true;
        gameOverCanvas.enabled = true;
    }

    // Salir del juego
    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Aplication.Quit();
        #endif
    }
}
