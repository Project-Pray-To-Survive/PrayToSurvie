using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    public void SetActiveInteract(bool active)
    {
        if (active != gameObject.activeInHierarchy)
        {
            gameObject.SetActive(active);
        }
    }
}
