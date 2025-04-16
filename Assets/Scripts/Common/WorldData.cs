using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class WorldData : MonoBehaviour
    {
        public Transform PoolContainer;
        public Transform WorldContainer => transform;
        public EnemyPositions EnemyPositions;
        public LevelBounds LevelBounds;
    }
}