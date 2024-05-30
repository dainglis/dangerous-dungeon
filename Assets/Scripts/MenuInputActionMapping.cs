using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuInputActionMapping : MonoBehaviour
{
    public UnityEvent MenuButtonPressed;

    public InputActionReference MenuAction;

    public PlayerInput PlayerActionMap;

    public Transform PauseMenu;


    public void Start()
    {
        MenuAction.action.performed += (obj) => MenuButtonPressed?.Invoke();
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
