using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


/// <summary>
///     UIElements controller for the top-level "menu" Document.
/// </summary>
/// <remarks>
///     Provides control to the nested <see cref="GraphicsUIContent"/> controller
/// </remarks>
public class MenuUIController : MonoBehaviour
{
    /// <summary>
    ///     Inner content controller. Soon will become a tabbed layout
    /// </summary>
    private readonly GraphicsUIContent graphicsUI = new();

    

    /// <summary> The root visual element from the <see cref="Document"/> </summary>
    private VisualElement root;

    /// <summary> Visual element displaying the menu buttons </summary>
    private VisualElement menuActions;

    /// <summary> Embedded visual element displaying settings </summary>
    private VisualElement graphicsUIElement;

    // Menu actions
    private Button settings;
    private Button restart;
    private Button quit;

    public UIDocument Document;

    [Header("Containers")]
    public GameObject Menu;

    [Header("Events")]
    public UnityEvent Restart;
    public UnityEvent Quit;


    public virtual void OnEnable()
    {
        root = Document.rootVisualElement;

        // Reference buttons
        settings = root.Q<Button>("buttonSettings");
        restart = root.Q<Button>("buttonRestart");
        quit = root.Q<Button>("buttonQuit");

        menuActions = root.Q<VisualElement>("actions");
        graphicsUIElement = root.Q<VisualElement>("graphicsMenu");

        settings.clicked += OnSettings;
        restart.clicked += OnRestart;
        quit.clicked += OnQuit;

        graphicsUI.enabled += HideActions;
        graphicsUI.disabled += ShowActions;
    }

    public virtual void OnDisable()
    {
        settings.clicked -= OnSettings;
        restart.clicked -= OnRestart;
        quit.clicked -= OnQuit;

        graphicsUI.enabled -= HideActions;
        graphicsUI.disabled -= ShowActions;
    }

    private void OnRestart() => Restart?.Invoke();

    private void OnQuit() => Quit?.Invoke();

    public void Open()
    {
        if (!Menu) { return; }
        Menu.SetActive(true);
    }

    public void Close()
    {
        if (!Menu) { return; }
        Menu.SetActive(false);
    }

    public void OnSettings()
    {
        graphicsUI.Enable(graphicsUIElement);
    }

    private void ShowActions()
    {
        menuActions.style.display = DisplayStyle.Flex;
    }

    private void HideActions()
    {
        menuActions.style.display = DisplayStyle.None;
    }
}
