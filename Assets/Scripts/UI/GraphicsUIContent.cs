using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
///     UIElements controller intended to be embedded within
///     a Document. Does not have a top-level Document itself.
/// </summary>
/// <remarks>
///     This class need not be a MonoBehaviour unless 
///     game lifecycle-related code is added.
/// </remarks>
public class GraphicsUIContent
{
    private VisualElement root;

    private Button confirm;
    private Button cancel;

    private DropdownField quality;

    public Action enabled;
    public Action disabled;

    public void Enable(VisualElement root)
    {
        this.root = root;

        root.style.display = DisplayStyle.Flex;

        confirm = root.Query<Button>("buttonConfirm");
        cancel = root.Query<Button>("buttonCancel");

        confirm.clicked += OnConfirm;
        cancel.clicked += OnCancel;

        InitQualitySettings();
        InitDisplayResolution();

        enabled?.Invoke();
    }

    public void Disable()
    {
        root.style.display = DisplayStyle.None;

        confirm.clicked -= OnConfirm;
        cancel.clicked -= OnCancel;

        disabled?.Invoke();
    }

    private void OnConfirm()
    {
        QualitySettings.SetQualityLevel(quality.index, true);
    }

    private void OnCancel()
    {
        Disable();
    }

    private void InitQualitySettings()
    {
        quality = root.Query<DropdownField>("dropdownQuality");

        quality.choices = QualitySettings.names.ToList();
        quality.index = QualitySettings.GetQualityLevel();
    }

    private void InitDisplayResolution()
    {
    }
}
