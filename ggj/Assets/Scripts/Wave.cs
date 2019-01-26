using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Wave
{
    public int Duration { get; }

    public int MinLevel { get; }

    public int MaxLevel { get; }

    public int EnemyCount { get; }

    public int Reward { get; }
    
    public int[] RespawnTimes { get; private set; }

    public Wave(int duration, int minLevel, int maxLevel, int enemyCount, int reward)
    {
        Duration = duration;
        MinLevel = minLevel;
        MaxLevel = maxLevel;
        EnemyCount = enemyCount;
        Reward = reward;
    }

    internal void CreateRespawnTimes()
    {
        List<int> _respawnTimes = new List<int>();
        for (int i = 0; i < EnemyCount; i++)
        {
            int time;
            do
            {
                time = Random.Range(0, Duration);
            }
            while (_respawnTimes.Contains(time));

            _respawnTimes.Add(time);
        }

        RespawnTimes = _respawnTimes.ToArray();
    }
}
