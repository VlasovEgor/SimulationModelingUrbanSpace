using UnityEngine;

public class ChangeTimeSpeed : MonoBehaviour
{
    [SerializeField] private float _timeMultiplier;

    private float _startFixedDeltaTime;
    private bool _isPause = false;

    private void Start()
    {
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void SetPauseStart()
    {
        if(_isPause == false)
        {
            _isPause = true;
            Time.timeScale = 0.00001f;
        }
        else
        {
            _isPause = false;
            Time.timeScale = 1;
        }

    }

    public void SetNormalTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
    }


    public void SetAcceleratedTime()
    {
        Time.timeScale = _timeMultiplier;
        Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
    }
}
