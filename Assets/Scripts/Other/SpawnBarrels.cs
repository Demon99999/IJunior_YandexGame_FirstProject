using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrels : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private ExplosionBarrel _template;
    [SerializeField] private Transform _container;

    private readonly List<ExplosionBarrel> _explosionBarrels = new List<ExplosionBarrel>();

    public void Create()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            ExplosionBarrel barrel = Instantiate(_template, _spawnPoints[i]);
            _explosionBarrels.Add(barrel);
        }
    }

    public void Clear()
    {
        foreach (var barrel in _explosionBarrels)
        {
            Destroy(barrel.gameObject);
        }

        _explosionBarrels.Clear();
    }
}
