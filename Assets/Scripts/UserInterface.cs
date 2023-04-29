using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] Image brainHealthBar;
    [SerializeField] Image heartHealthBar;
    [SerializeField] Image liverHealthBar;
    [SerializeField] Image kidneysHealthBar;
    [SerializeField] Image stomachHealthBar;

    private Organ brain;
    private Heart heart;
    private Liver liver;
    private Kidneys kidneys;
    private Stomach stomach;

    // Start is called before the first frame update
    void Start()
    {
        try { brain = GameObject.Find("Brain").GetComponent<Organ>(); } catch { brain = null; }
        try { heart = GameObject.Find("Heart").GetComponent<Heart>(); } catch { heart = null; }
        try { liver = GameObject.Find("Liver").GetComponent<Liver>(); } catch { liver = null; }
        try { kidneys = GameObject.Find("Kidneys").GetComponent<Kidneys>(); } catch { kidneys = null; }
        try { stomach = GameObject.Find("Stomach").GetComponent<Stomach>(); } catch { stomach = null; }
    }

    // Update is called once per frame
    void Update()
    {
        if (brain)
            brainHealthBar.fillAmount = brain.health / brain.maxHealth;
        if (heart)
            heartHealthBar.fillAmount = heart.health / heart.maxHealth;
        if (liver)
            liverHealthBar.fillAmount = liver.health / liver.maxHealth;
        if (kidneys)
            kidneysHealthBar.fillAmount = kidneys.health / kidneys.maxHealth;
        if (stomach)
            stomachHealthBar.fillAmount = stomach.health / stomach.maxHealth;
    }
}
