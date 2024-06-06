using UnityEngine;

public class MenuInputActionMapping : MonoBehaviour
{

    public Transform PauseMenu;



    public void Close()
    {
        PauseMenu.gameObject.SetActive(false);
    }

    public void Open()
    {
        PauseMenu.gameObject.SetActive(true);
    }
}
