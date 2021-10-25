using UnityEngine;

public class InstancePlayer : MonoBehaviour
{
    public static InstancePlayer instance;

    private void Awake() 
    {
        instance = this;
    }
}
