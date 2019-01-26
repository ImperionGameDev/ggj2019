using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStatus _status = GameStatus.MainMenu;
    public GameStatus Status
    {
        get
        {
            return _status;
        }
        set
        {
            if (value == Status)
            {
                return;
            }
            _status = value;
            OnStatusChanged();
        }
    }

    public GameObject[] MainMenuObjects;

    public GameObject[] GameObjects;

    public GameObject[] PauseObjects;

    // Start is called before the first frame update
    void Start()
    {
        EnableDisableObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClick()
    {
        Status = GameStatus.Game;
    }

    private void OnStatusChanged()
    {
        EnableDisableObjects();
    }

    private void EnableDisableObjects()
    {
        bool active;

        active = Status == GameStatus.MainMenu;
        foreach (var x in MainMenuObjects)
        {
            x.SetActive(active);
        }

        active = Status == GameStatus.Game;
        foreach (var x in GameObjects)
        {
            x.SetActive(active);
        }

        active = Status == GameStatus.Pause;
        foreach (var x in PauseObjects)
        {
            x.SetActive(active);
        }
    }
}
