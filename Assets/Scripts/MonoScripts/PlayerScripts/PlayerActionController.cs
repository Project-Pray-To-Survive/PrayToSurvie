using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public PlayerStaminaLogic PlayerStaminaLogic{get; private set;}
    public PlayerSprintLogic PlayerSprintLogic{get; private set;}

    public void InitializeStamina(PlayerStatus playerStatus)
    {
        PlayerStaminaLogic = new PlayerStaminaLogic(playerStatus);
    }
    public void InitializeSprint(PlayerStatus playerStatus)
    {
        PlayerSprintLogic = new PlayerSprintLogic(playerStatus);
    }
    
}
