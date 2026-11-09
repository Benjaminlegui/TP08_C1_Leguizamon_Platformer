using System;

public interface IPoolable
{
    void OnCreated(Action release);
    void OnGet();
    void OnRelease();
}
