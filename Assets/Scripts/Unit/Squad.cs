using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private AimCursor _aimCursor;
    
    private List<Cell> _cells;
    private int _maxUnit = 5;
    private List<Unit> _units;
    private List<Unit> _unitsSquad;
    
    private void Start()
    {
        _cells =new List<Cell>();
        _units=new List<Unit>();
        _unitsSquad=new List<Unit>();
    }

    public void ReturnUnits()
    {
       DeleteUnits();
    }

    public void HideUnitsLevel()
    {
        foreach (var unit in _units)
        {
            if (unit.TryGetComponent(out UnitLevel unitLevel))
            {
                unitLevel.HideLevel();
            }
        }
    }

    public void UnitsMove()
    {
        _cells = _gridGenerator.Cells;
        _units = TryGetUnits(_cells);
        DistributionMove(_maxUnit, _units);
    }

    private void DeleteUnits()
    {
        foreach (var unit in _units)
        {
            Destroy(unit.gameObject);
        }

        _units.Clear();
    }

    private List<Unit> TryGetUnits(List<Cell> cells)
    {
        List<Unit> units = new List<Unit>();

        foreach (var point in cells)
        {
            if (point.gameObject.GetComponentInChildren<Unit>())
            {
                Unit unit = point.gameObject.GetComponentInChildren<Unit>();
                unit.GetComponent<ShootState>().Init(_aimCursor);
                units.Add(unit);
            }
        }

        return units;
    }

    private void DistributionMove(int count,List<Unit> units)
    {
        if (units.Count >= count)
        {
            AddUnits(count,units);
        }
        else
        {
            AddUnits(units.Count,units);
        }
    }

    private void AddUnits(int count, List<Unit> units)
    {
        List<Vector3> firingLine = GetPointPosition(count);

        for (int i = 0; i < count; i++)
        {
            _unitsSquad.Add(units[i]);
            units[i].Init(firingLine[i]);
            units[i].Move();
            units[i].GetComponent<Collider>().enabled = false;
        }
    }

    private List<Vector3> GetPointPosition(int count)
    {
        List<Vector3> points = new List<Vector3>();

        for (float i = 1; i < count * 2; i = i + 1.5f)
        {
            points.Add(new Vector3(i, 0, 4f));
        }

        return points;
    }
}
