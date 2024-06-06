using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphicsUI : MonoBehaviour
{
    private const string ConfirmName = "buttonConfirm";
    private const string CancelName = "buttonCancel";

    [SerializeField] private string confirmName = ConfirmName;
    [SerializeField] private string cancelName = CancelName;
    private UIDocument document;

    private Button confirm;
    private Button cancel;

    
    /// <summary>
    ///     Connect UI script to document on enable, subscribing to click events
    /// </summary>
    private void OnEnable()
    {
        document = GetComponent<UIDocument>();

        confirm = document.rootVisualElement.Query<Button>(confirmName);
        cancel = document.rootVisualElement.Query<Button>(cancelName);

        confirm.clicked += OnConfirm;
        cancel.clicked += OnCancel;
    }

    /// <summary>
    ///     Unsubscribe from click events
    /// </summary>
    private void OnDisable()
    {
        confirm.clicked -= OnConfirm;
        cancel.clicked -= OnCancel;
    }

    private void OnConfirm()
    {
        
    }

    private void OnCancel()
    {
        gameObject.SetActive(false);
    }
}
