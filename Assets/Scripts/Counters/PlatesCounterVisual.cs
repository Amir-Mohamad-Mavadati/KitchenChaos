using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform PlateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;
    private List<GameObject> PlateVisualList;

    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        PlateVisualList = new List<GameObject>();
    }

    private void PlatesCounter_OnPlateSpawn(object Sender, System.EventArgs e)
    {
        Transform Plate = Instantiate(PlateVisualPrefab, CounterTopPoint);
        float PlateOffsetY = .1f;
        Plate.localPosition = new Vector3(0, PlateOffsetY * PlateVisualList.Count, 0);
        PlateVisualList.Add(Plate.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object Sender, System.EventArgs e)
    {
        GameObject RemovePlatGameObj = PlateVisualList[PlateVisualList.Count - 1];
        PlateVisualList.Remove(RemovePlatGameObj);
        Destroy(RemovePlatGameObj);
    }
}
