using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private BloodCellManager bloodManager;
    [SerializeField] private int numberOfCells = 5;
    [SerializeField] private float distanceToCenter = 1.5f;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject cell;
    private List<GameObject> cells;
    private int visibleCells = 0;

    // Start is called before the first frame update
    void Start()
    {
        cells = new List<GameObject>();
        for (int i = 0; i < numberOfCells; i++) {
            float angle = i * (2 * Mathf.PI / numberOfCells);
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 position = transform.position + direction * distanceToCenter;
            GameObject orbiter = Instantiate(cell, position, Quaternion.identity, transform);
            orbiter.transform.localScale = new Vector3 (.2f, .2f, .2f);
            orbiter.GetComponent<Renderer>().enabled = false;
            orbiter.transform.parent = transform;
            cells.Add(orbiter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    public void AddCell() {
        if (bloodManager.oxyBloodCellCount > visibleCells && bloodManager.oxyBloodCellCount <= numberOfCells) {
            cells[visibleCells].GetComponent<Renderer>().enabled = true;
            visibleCells++;
        }
        visibleCells = Mathf.Clamp(visibleCells, 0, numberOfCells);
    }

    public void RemoveCell() {
        if (bloodManager.oxyBloodCellCount < visibleCells) {
            visibleCells--;
            cells[visibleCells].GetComponent<Renderer>().enabled = false;
        }
        visibleCells = Mathf.Clamp(visibleCells, 0, numberOfCells);
    }
}
