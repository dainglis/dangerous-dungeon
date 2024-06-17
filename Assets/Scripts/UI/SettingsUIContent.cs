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
public class SettingsUIContent
{
    private VisualElement root;

    private Button applyButton;
    private Button closeButton;

    private DropdownField qualitySelection;

    public Action Enabled;
    public Action Disabled;


    /// <summary>
    ///     Configures the UI controller based on the given <paramref name="root"/> element
    /// </summary>
    /// <remarks>
    ///     Note that this is *not* the Unity OnEnable() callback. Instead this function is
    ///     called from a parent controller that is likely a GameObject
    /// </remarks>
    public void Enable(VisualElement root)
    {
        this.root = root;

        root.style.display = DisplayStyle.Flex;

        applyButton = root.Query<Button>("buttonApply");
        closeButton = root.Query<Button>("buttonClose");

        applyButton.clicked += OnApply;
        closeButton.clicked += OnCancel;

        InitGraphicsSettings();

        Enabled?.Invoke();
    }

    public void Disable()
    {
        root.style.display = DisplayStyle.None;

        applyButton.clicked -= OnApply;
        closeButton.clicked -= OnCancel;

        Disabled?.Invoke();
    }

    private void OnApply()
    {
        ApplyGraphicsSettings();
    }

    private void OnCancel()
    {
        Disable();
    }

    private void InitGraphicsSettings()
    {
        // Quality settings
        qualitySelection = root.Query<DropdownField>("dropdownQuality");

        qualitySelection.choices = QualitySettings.names.ToList();
        qualitySelection.index = QualitySettings.GetQualityLevel();
    }

    private void ApplyGraphicsSettings()
    {
        QualitySettings.SetQualityLevel(qualitySelection.index, true);

    }
}
