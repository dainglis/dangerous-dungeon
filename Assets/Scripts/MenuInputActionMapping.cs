using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class MenuInputActionMapping : MonoBehaviour
{
    public UnityEvent MenuButtonPressed;

    public Transform PauseMenu;

    public void HandleInput(CallbackContext context)
    {
        if (context.performed) { MenuButtonPressed?.Invoke(); }
    }


    public void Play()
    {
        PauseMenu.gameObject.SetActive(false);
    }

    public void Pause()
    {
        PauseMenu.gameObject.SetActive(true);
    }
}
