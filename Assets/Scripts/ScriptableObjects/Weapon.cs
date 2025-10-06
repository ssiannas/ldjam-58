using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "SO/Weapon")]
    public class Weapon : ScriptableObject
    {
        public PlayerWeapons WeaponType;
        public GameObject SwooshPrefab;
    }
}
