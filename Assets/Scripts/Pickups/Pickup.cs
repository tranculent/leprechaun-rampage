using UnityEngine;

// Base class for all pickup types
public abstract class Pickup : MonoBehaviour
{
    [Range(0f, 1f)]
    public float _spawnProbability = Consts.DEFAULT_PICKUP_PROBABILITY;
    public float _spawnDuration = -1f;

}