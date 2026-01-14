using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusData", menuName = "Scriptable Objects/PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    public float moveSpeed;
    public float jumpSpeed;
    public float maxStamina;
}
