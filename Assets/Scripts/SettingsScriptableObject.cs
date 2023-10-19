using UnityEngine;

[CreateAssetMenu(fileName = "Standart Settings", menuName = "ScriptableObjects/Game Standart Settings")]
public class SettingsScriptableObject : ScriptableObject
{
    public float _playerStartSpeed;
    public float _playerMaxSpeed;
    public float _playerSpeedAcceleration;
    
}
