using UnityEngine;

public class UIConnecter : MonoBehaviour,IConnector
{
    [SerializeField] private PlayerInteractionController playerInteractionController;
    [SerializeField] private InteractionButton interactionButton;
    void Start()
    {
        OnConnect();
    }

    public void OnConnect()
    {
        playerInteractionController.OnButtonSet += interactionButton.SetActiveInteract;
    }

    public void OnDisconnect()
    {
        playerInteractionController.OnButtonSet -= interactionButton.SetActiveInteract;
    }
}
