using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _prefabSniper;
    [SerializeField] private Unit _prefabRifl;
    [SerializeField] private Unit _prefabBazuka;
    [SerializeField] private GridGenerator _gridGenerator;
    
    private int _startPriseSniper = 100;
    private int _startPriseRifl = 150;
    private int _startPriceBazuka = 200;
    private int _percent = 100;
    private int _percentageIncrease = 10;

    private int _priceSniper;
    private int _priceRifl;
    private int _priceBazuka;
    
    private List<Cell> _cells;

    public event UnityAction<int> PriseSniperChanged;
    public event UnityAction<int> PriseRiflChanged;
    public event UnityAction<int> PriseBazukaChanged;

    private void Start()
    {
        ResetPrise();
        
        PriseSniperChanged?.Invoke(_priceSniper);
        PriseRiflChanged?.Invoke(_priceRifl);
        PriseBazukaChanged?.Invoke(_priceBazuka);

        _cells =new List<Cell>();
        _cells = _gridGenerator.Cells;
    }

    public void SpawnSniper()
    {
        SpawnUnit(ref _priceSniper,_prefabSniper);
        PriseSniperChanged?.Invoke(_priceSniper);
    }

    public void SpawnRifl()
    {
        SpawnUnit(ref _priceRifl, _prefabRifl);
        PriseRiflChanged?.Invoke(_priceRifl);
    }

    public void SpawnBazuka()
    {
        SpawnUnit(ref _priceBazuka, _prefabBazuka);
        PriseBazukaChanged?.Invoke(_priceBazuka);
    }

    public void ResetPrise()
    {
        _priceSniper = _startPriseSniper;
        _priceRifl = _startPriseRifl;
        _priceBazuka = _startPriceBazuka;

        PriseSniperChanged?.Invoke(_priceSniper);
        PriseRiflChanged?.Invoke(_priceRifl);
        PriseBazukaChanged?.Invoke(_priceBazuka);
    }

    public void Spawn(Unit unit)
    {
        Transform point = GetPoint();

        if (point != null)
        {
            Unit unitSpawn = null;
            unitSpawn = Instantiate(unit, point.position,Quaternion.identity);
            unitSpawn.transform.parent = point.transform;
        }
    }

    private Transform GetPoint()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].IsEmployed == false)
            {
                _cells[i].ChangeValue();
                return _cells[i].transform;
            }
        }

        return null;
    }

    private void SpawnUnit(ref int prise, Unit unit)
    {
        if (Wallet.Money >= prise)
        {
            Spawn(unit);
            Wallet.ChangeMoney(-prise);
            RaisPrice(ref prise);
        }
    }

    private void RaisPrice(ref int prise)
    {
        prise += (prise / _percent) * _percentageIncrease;
    }
}
