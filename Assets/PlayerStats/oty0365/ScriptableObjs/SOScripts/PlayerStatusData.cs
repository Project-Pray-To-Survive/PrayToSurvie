using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "Scriptable Objects/PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    public float moveSpeed;
    public float sprintSpeed;
    public float jumpSpeed;
    public float maxStamina;
    public float staminaDecreaseSpeed;
    public float staminaFillSpeed;
    [Range(0,100)]public float staminaRegenRate;
}
