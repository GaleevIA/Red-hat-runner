using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public PlatformData platformData;
    private PlatformModel _platformModel;
    private PlatformView _platformView;

    private void Start()
    {
        _platformModel = new PlatformModel();
        _platformView = new PlatformView();
    }
}
