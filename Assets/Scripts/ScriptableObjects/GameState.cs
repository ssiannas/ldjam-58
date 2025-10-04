using System;
using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "GameState", menuName = "SO/GameState")]
    public class GameState : ScriptableObject
    {
         public float SpawnInterval = 2.0f;

         [Header("Reset Values")]
         [SerializeField] float defaultSpawnInterval = 2.0f;

         public void Reset()
         {
             SpawnInterval = defaultSpawnInterval;
         }
    }
}
 