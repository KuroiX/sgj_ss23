using System;

public struct BoolEvent
{
    public event Action<bool> EventTriggered;
    
    public bool IsActive
    {
        get => _isActive;
        set
        {
            OnEventTriggered(value);
            _isActive = value;
        }
    }
    
    private bool _isActive;
    
    private void OnEventTriggered(bool isPaused)
    {
        EventTriggered?.Invoke(isPaused);
    }
}