using System;

public interface IView {
    void OnMessage(IMessage message);
    void OnEnter();
    void OnExit();
    void OnPause();
    void OnResume();
}
