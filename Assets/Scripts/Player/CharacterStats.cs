using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public enum Stat
    {
        Level = 0,
        Vitality = 1,       // Points
        Attunement = 2,     // Ability
        Endurance = 3,      // Movement Speed
        Strength = 4,       // Damage / Projectile Speed?
        Dexterity = 5,      // Fire Rate
        Resistance = 6,     // Defense
        Intelligence = 7,   // Range
        Faith = 8,          // ?
        Humanity = 9        // ?
    }

    public Dictionary<Stat, int> Values;

    // hmmmmmm
    public static CharacterStats DefaultCharacterStats()
    {
        CharacterStats stats = new()
        {
            Values = new Dictionary<Stat, int>()
        };

        stats.Values.Add(Stat.Level, 0);
        stats.Values.Add(Stat.Vitality, 0);
        stats.Values.Add(Stat.Attunement, 0);
        stats.Values.Add(Stat.Endurance, 50);
        stats.Values.Add(Stat.Strength, 70);
        stats.Values.Add(Stat.Dexterity, 60);
        stats.Values.Add(Stat.Resistance, 0);
        stats.Values.Add(Stat.Intelligence, 0);
        stats.Values.Add(Stat.Faith, 0);
        stats.Values.Add(Stat.Humanity, 0);

        return stats;
    }

    [Tooltip("Total Level")]
    public int Level = 0;

    [Tooltip("Movement Speed")]
    [Range(0, 200)]
    public int Endurance = 50;

    [Tooltip("Projectile Speed")]
    [Range(0, 200)]
    public int Strength = 70;

    [Tooltip("Projectile Fire Rate")]
    [Range(0, 200)]
    public int Dexterity = 60;

    [Tooltip("Projectile Range")]
    [Range(0, 200)]
    public int Intelligence = 50;

}
