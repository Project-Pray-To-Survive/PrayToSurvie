using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerProceduralAnimaiton : MonoBehaviour
{
    [SerializeField] private Rig leftAnimatedArmRig;
    [SerializeField] private Rig rightAnimatedArmRig;
    [SerializeField] private Rig leftArmRig;
    [SerializeField] private Rig rightArmRig;
    [SerializeField] private Transform leftArmTarget;
    [SerializeField] private Transform rightArmTarget;
    
    private Transform _leftArmTransform;
    private Transform _rightArmTransform;
    

    public void SetLeftArmTarget(Transform lat)
    {
        leftAnimatedArmRig.weight = 0;
        leftArmRig.weight = 1;
        _leftArmTransform = lat;
    }
    
    public void SetRightArmTarget(Transform rat)
    {
        rightAnimatedArmRig.weight = 0;
        rightArmRig.weight = 1;
        _rightArmTransform = rat;
    }
    
    public void FreeLeftArmTarget()
    {
        leftAnimatedArmRig.weight = 1;
        leftArmRig.weight = 0;
        _leftArmTransform = null;
    }
    
    public void FreeRightArmTarget()
    {
        rightAnimatedArmRig.weight = 1;
        rightArmRig.weight = 0;
        _rightArmTransform = null;
    }
    
    private void LateUpdate()
    {
        if (_leftArmTransform == null || _rightArmTransform == null) return;
        leftArmTarget.position = _leftArmTransform.position;
        rightArmTarget.position = _rightArmTransform.position;
    }
}
