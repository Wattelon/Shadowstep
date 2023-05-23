using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    [SerializeField] private InputActionProperty showMenu;

    private void Update()
    {
        if (showMenu.action.WasPerformedThisFrame())
        {
            menu.enabled = !menu.enabled;
        }
    }

    public void OnRestartButtonCLick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
