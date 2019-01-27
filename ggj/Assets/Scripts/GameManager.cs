using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    // TODO(batuhan): IDK 
    public TowerSelectionUI m_ShopController;
    public bool IsShopOpen
    {
        get;
        private set;
    }

    public GameObject[] MainMenuObjects;

    public GameObject[] GameObjects;

    public GameObject[] PauseObjects;

    private int _currentWave = -1;
    public int CurrentWave
    {
        get
        {
            return _currentWave;
        }
        set
        {
            if (_currentWave == value)
            {
                return;
            }

            _currentWave = value;
            OnCurrentWaveChanged();
        }
    }

    public Wave[] Waves { get; } = new Wave[]
    {
        new Wave(10, 1, 1, 3, 1),
        new Wave(15, 1, 2, 4, 2),
        new Wave(20, 2, 3, 5, 4),
        new Wave(25, 2, 4, 6, 6),
        new Wave(30, 3, 5, 7, 9),
        new Wave(35, 4, 6, 8, 11),
    };

    public float preperationDuration = 15;

    private float _elapsed;
    public float Elapsed
    {
        get
        {
            return _elapsed;
        }
        set
        {
            if (_elapsed == value)
            {
                return;
            }
            _elapsed = value;
            OnElapsedChanged();
        }
    }

    public Text statusText;

    public Text leftTimeText;

    public Text waveText;

    public EnemySpawner EnemySpawner { get; private set; }

    // Start is called before the first frame update
    void Start()
    {             
        EnemySpawner = this.GetComponent<EnemySpawner>();
        EnableDisableObjects();
        UpdateStatusText();
        UpdateElapsedText();
        UpdateWaveText();

        OnStatusChanged();
    }

    // Update is called once per frame
    void Update()
    {
        Elapsed += Time.deltaTime;

        if (Status == GameStatus.Preperation)
        {
            if (Elapsed >= preperationDuration)
            {
                Status = GameStatus.Wave;
            }
        }
        else if (Status == GameStatus.Wave)
        {
            if (EnemySpawner.Status == SpawnerStatus.Generated
                && EnemySpawner.RespawnedEnemies.All(x => x.GetComponent<Enemy>().CurrentHealth <= 0))
            {
                EnemySpawner.enabled = false;
                Status = GameStatus.Preperation;
            }
        }
    }

    public void OpenShopClick()
    {
        IsShopOpen = m_ShopController.Open();
    }

    public void PlayClick()
    {
        Elapsed = 0;
        CurrentWave = -1;
        Status = GameStatus.Preperation;
    }

    private void OnStatusChanged()
    {
        EnableDisableObjects();
        UpdateStatusText();

        Elapsed = 0;
        if (Status == GameStatus.Wave)
        {
            WaveStarted();
        }
        else if (Status == GameStatus.Preperation)
        {
            EnableEditMode();
        }
    }

    private void EnableEditMode()
    {
        GameObject.Find("Map").GetComponent<MapTileGenerator>().EnableEditMode();
    }

    private void OnElapsedChanged()
    {
        UpdateElapsedText();
    }

    private void OnCurrentWaveChanged()
    {
        UpdateWaveText();
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = (CurrentWave + 1).ToString();
        }
    }

    private void UpdateElapsedText()
    {
        if (leftTimeText != null)
        {
            float time;
            if (Status == GameStatus.Preperation)
            {
                time = preperationDuration - Elapsed;
                if (time < 3)
                {
                    leftTimeText.text = time.ToString("00.00", CultureInfo.InvariantCulture);
                    leftTimeText.color = Color.red;

                    // TODO(Batuhan): Delete me;
                    OpenShopClick();
                }
                else
                {
                    leftTimeText.text = time.ToString("00", CultureInfo.InvariantCulture);
                    leftTimeText.color = Color.white;
                }
            }
            else if (Status == GameStatus.Wave)
            {
                time = Elapsed;

                leftTimeText.text = time.ToString("00", CultureInfo.InvariantCulture);
                leftTimeText.color = Color.white;
            }
            else
            {
                time = 0;

                leftTimeText.text = time.ToString("00", CultureInfo.InvariantCulture);
                leftTimeText.color = Color.white;
            }

        }
    }

    private void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = Status.ToString();
            if (Status == GameStatus.Preperation || Status == GameStatus.Wave)
            {
                statusText.color = Color.green;
            }
            else
            {
                statusText.color = Color.white;
            }
        }
    }

    private void WaveStarted()
    {
        CurrentWave++;

        var wave = Waves.Length < CurrentWave ? null : Waves[CurrentWave];

        if (wave == null)
        {
            //TODO: Game should be ended
        }

        EnemySpawner.SetWave(wave);
    }

    private void EnableDisableObjects()
    {
        bool active;

        active = Status == GameStatus.MainMenu;
        foreach (var x in MainMenuObjects)
        {
            x.SetActive(active);
        }

        active = Status == GameStatus.Preperation || Status == GameStatus.Wave || Status == GameStatus.Edit;
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
