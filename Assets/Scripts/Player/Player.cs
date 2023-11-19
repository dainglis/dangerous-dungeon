using UnityEngine;

public class Player : MonoBehaviour
{
    public InputActionMapping InputActions;
    public CharacterController Controller;
    public CharacterStats Stats;

    // char controller
    // char stats
    // movement input

    private void OnValidate()
    {
        if (!InputActions) { InputActions = GetComponent<InputActionMapping>(); }
        if (!Controller) { Controller = GetComponent<CharacterController>(); }
    }


    public void Update()
    {
        ReadStats();
    }

    public void ReadStats()
    {
        InputActions.MovementSpeed = CharacterStatsHandler.Convert(Stats.Endurance);
        InputActions.ProjectileRate = CharacterStatsHandler.Convert(Stats.Dexterity);
        InputActions.ProjectileSpeed = CharacterStatsHandler.Convert(Stats.Strength);
    }
}
