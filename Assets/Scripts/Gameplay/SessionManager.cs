using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class SessionManager : MonoBehaviour
{
    private bool m_StateDirty = true;
    private PlayState m_State = PlayState.Playing;

    /// <summary>
    ///     The current gameplay state
    /// </summary>
    /// <remarks>
    ///     This reuses a useful but unrelated enum from 
    ///     the <see cref="UnityEngine.Playables"/> library
    /// </remarks>
    public PlayState State
    {
        get => m_State;
        set
        {
            m_State = value;
            m_StateDirty = true;
        }
    }
    
    public bool StartPaused = false;

    public UnityEvent Played;
    public UnityEvent Paused;

    void Start()
    {
        if (StartPaused)
        {
            State = PlayState.Paused;
        }
    }

    void Update()
    {
        if (m_StateDirty)
        {
            UpdatePlayState();
        }
    }

    private void UpdatePlayState()
    {
        m_StateDirty = false;

        switch (m_State)
        {
            case PlayState.Playing:
                Play();
                break;
            case PlayState.Paused:
                Pause();
                break;
        }
    }

    public void HandleInput(CallbackContext context)
    {
        if (context.performed) { Toggle(); }
    }

    public void Toggle()
    {
        State = (m_State == PlayState.Playing)
            ? State = PlayState.Paused
            : State = PlayState.Playing;
    }

    private void Play()
    {
        Time.timeScale = 1.0f;
        Played?.Invoke();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        Paused?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit(0);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
