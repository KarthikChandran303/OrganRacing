using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BloodCellManager : MonoBehaviour
{
    public static BloodCellManager instance;

    public int oxyBloodCellCount = 0;
    public int unoxyBloodCellCount = 0;

    [SerializeField] TMP_Text oxyBloodCellCountLabel;
    [SerializeField] TMP_Text unoxyBloodCellCountLabel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddBloodCell(int amount = 1)
    {
        oxyBloodCellCount += amount;
        UpdateLabels();
    }

    public void AddUnoxyBloodCell(int amount = 1)
    {
        unoxyBloodCellCount += amount;
        UpdateLabels();
    }

    public void UseBloodCell(int amount = 1)
    {
        if (amount > oxyBloodCellCount)
            amount = oxyBloodCellCount;

        oxyBloodCellCount -= amount;
        UpdateLabels();
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
        UpdateLabels();

        return amount;
    }

    public int DeliverUnoxygenatedCells()
    {
        int amount = unoxyBloodCellCount;
        unoxyBloodCellCount -= amount;
        UpdateLabels();
        return amount;
    }

    public void UpdateLabels()
    {
        oxyBloodCellCountLabel.text = "Oxygenated Blood Cells: " + oxyBloodCellCount;
        unoxyBloodCellCountLabel.text = "Unoxygenated Blood Cells: " + unoxyBloodCellCount;
    }

}
