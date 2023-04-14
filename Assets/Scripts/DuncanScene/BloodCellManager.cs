using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BloodCellManager : MonoBehaviour
{
    public static BloodCellManager instance;

    public int oxyBloodCellCount = 0;
    public int unoxyBloodCellCount = 0;

    public GameObject oxyCellHolder;

    public GameObject unoxyCellHolder;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
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
    }

    public void UseBloodCell(int amount = 1)
    {
        if (amount > oxyBloodCellCount)
            amount = oxyBloodCellCount;

        oxyBloodCellCount -= amount;
        updateCellCount();
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
