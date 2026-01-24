using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private GrabableItem _currentItem;
    private PlayerProceduralAnimation _playerProceduralAnimation;

    public void Observe(GrabableItem item)
    {
        _currentItem = item;
    }

    public void Put()
    {
        _playerProceduralAnimation = gameObject.GetComponent<PlayerProceduralAnimation>();
        _playerProceduralAnimation.SetLeftArmTarget(_currentItem.LeftHandTransform);
        _playerProceduralAnimation.SetRightArmTarget(_currentItem.RightHandTransform);
        
        _currentItem.transform.SetParent(gameObject.transform);
        _currentItem.transform.localPosition = _currentItem.GrabbOffset;
        _currentItem.transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        _playerProceduralAnimation.FreeLeftArmTarget();
        _playerProceduralAnimation.FreeRightArmTarget();
        _currentItem.transform.SetParent(null);
    }

}
