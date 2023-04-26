using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BloodCellManager : MonoBehaviour
{
    public static BloodCellManager instance;

    public int oxyBloodCellCount = 0;
    public int unoxyBloodCellCount = 0;
    public int oxyCount = 0;

    public GameObject oxyCellHolder;

    public GameObject unoxyCellHolder;
    [SerializeField] private GameObject pickupPositions;
    [SerializeField] private GameObject oxyPickup;
    private Dictionary<Transform, GameObject> oxyInstances = new();
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private List<GameObject> spawnablePositions;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        InvokeRepeating("Spawn", 0, spawnRate);
    }
    private void Spawn() {
        if(oxyInstances.Count == pickupPositions.transform.childCount || pickupPositions.transform.childCount == 0) {
            return;
        }
        // while(true) {
        //     Transform pos = pickupPositions.transform.GetChild(Random.Range(0, pickupPositions.transform.childCount));
        //     if(!oxyInstances.ContainsKey(pos)) {
        //         GameObject cell = Instantiate(oxyPickup, pos);
        //         oxyInstances.Add(pos, cell);
        //         break;
        //     }
        // }
        GameObject spawnPos = spawnablePositions[Random.Range(0, spawnablePositions.Count)];
        Mesh mesh = spawnPos.GetComponent<MeshCollider>().sharedMesh;
        Vector3 randomPosition = spawnPos.transform.TransformPoint(mesh.vertices[Random.Range(0, mesh.vertexCount)]);
        randomPosition = new Vector3(randomPosition.x, randomPosition.y + 1.5f, randomPosition.z);
        GameObject cell = Instantiate(oxyPickup, randomPosition, Quaternion.identity);
        // RaycastHit hit;
        // if (Physics.Raycast(cell.transform.GetChild(2).position, Vector3.down, out hit)) {
        //     cell.transform.GetChild(2).rotation = Quaternion.FromToRotation(cell.transform.GetChild(2).up, hit.normal);
        // }
        cell.transform.parent = transform;
        oxyInstances.Add(cell.transform, cell);
    }

    public void Pickup(Transform pos) {
        oxyInstances.Remove(pos);
    }

    public int BloodCellCount()
    {
        return oxyBloodCellCount + unoxyBloodCellCount;
    }

    public void addOxyCell() {
        for (int i = 0; i < oxyBloodCellCount; i++) {
            oxyCellHolder.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void addUnoxyCell() {
        for (int i = oxyBloodCellCount; i < oxyBloodCellCount + unoxyBloodCellCount; i++) {
            unoxyCellHolder.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // public void removeOxyCell() {
    //     oxyCellHolder.transform.GetChild(oxyBloodCellCount).gameObject.SetActive(false);
    // }

    public void updateCellCount() {
        for (int i = 0; i < 12; i++) {
            oxyCellHolder.transform.GetChild(i).gameObject.SetActive(false);
            unoxyCellHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
        addOxyCell();
        addUnoxyCell();
    }

    public void AddBloodCell(int amount = 1)
    {
        oxyBloodCellCount += amount;
        updateCellCount();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Orbit>().AddCell();
    }

    public void AddUnoxyBloodCell(int amount = 1)
    {
        unoxyBloodCellCount += amount;
        updateCellCount();
    }

    public void OxygenateCells()
    {
        oxyBloodCellCount += unoxyBloodCellCount;
        unoxyBloodCellCount = 0;
        updateCellCount();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Orbit>().AddCell();
    }

    public void AlveoliOxgenate()
    {
        oxyCount++;
        if (oxyCount > 4) {
            oxyCount = 0;
            if (unoxyBloodCellCount > 0) {
                unoxyBloodCellCount--;
                oxyBloodCellCount++;
                updateCellCount();
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Orbit>().AddCell();
            }
        }
    }

    public void UseBloodCell(int amount = 1)
    {
        if (amount > oxyBloodCellCount)
            amount = oxyBloodCellCount;

        oxyBloodCellCount -= amount;
        updateCellCount();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Orbit>().RemoveCell();
    }

    /// <summary>
    /// Uses the given number of oxygenated blood cells and returns the amount used.
    /// If the requested amount exceeeds the total number of oxygenated blood cells,
    /// uses all of the oxygenated blood cells.
    /// </summary>
    /// <param name="amount">the amount of oxygenated blood cells to use</param>
    /// <returns>the amount used</returns>
    public int UseOxygen(int amount = 1)
    {
        if (amount > oxyBloodCellCount)
            amount = oxyBloodCellCount;

        unoxyBloodCellCount += amount;
        oxyBloodCellCount -= amount;
        updateCellCount();
        return amount;
    }

    public int DeliverUnoxygenatedCells()
    {
        int amount = unoxyBloodCellCount;
        unoxyBloodCellCount -= amount;
        updateCellCount();
        return amount;
    }

    // public void UpdateLabels()
    // {
    //     oxyBloodCellCountLabel.text = "Oxygenated Blood Cells: " + oxyBloodCellCount;
    //     unoxyBloodCellCountLabel.text = "Unoxygenated Blood Cells: " + unoxyBloodCellCount;
    // }

}
