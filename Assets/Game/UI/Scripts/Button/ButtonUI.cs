using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    private ICallback _callback;

    public void Show(object args = null, ICallback callback = null)
    {
        _callback = callback;
        OnShow(args);
    }

    public void Hide()
    {
        OnHide();
    }

    protected virtual void OnShow(object args)
    {
    }

    protected virtual void OnHide()
    {
    }

    public void RequestClose()
    {
        _callback?.OnClose(this);
    }

    public void OnClose(ButtonUI button)
    {
        throw new System.NotImplementedException();
    }

    public interface ICallback
    {
        void OnClose(ButtonUI button);
    }
}
