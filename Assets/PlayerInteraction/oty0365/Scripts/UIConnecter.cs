using System;
using UnityEngine;

public class UIConnecter : MonoBehaviour,IConnector
{
    [SerializeField] private PlayerInteractionController playerInteractionController;
    [SerializeField] private PlayerStaminaUI playerStaminaUI;
    [SerializeField] private InteractionButton interactionButton;
    [SerializeField] private PlayerStatus playerStatus;
    
    void Start()
    {
        OnConnect();
        playerStaminaUI.SetSlide(playerStatus.stamina.Value,playerStatus.stamina.MaxValue);
    }

    public void OnConnect()
    {
        playerInteractionController.OnButtonSet += interactionButton.SetActiveInteract;
        playerStatus.stamina.OnValueChange += playerStaminaUI.SetSlide;
    }

    public void OnDisconnect()
    {
        playerInteractionController.OnButtonSet -= interactionButton.SetActiveInteract;
        playerStatus.stamina.OnValueChange -= playerStaminaUI.SetSlide;
    }

    private void OnDestroy() => OnDisconnect();
}
