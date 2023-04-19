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
        brain = GameObject.Find("Brain").GetComponent<Organ>();
        heart = GameObject.Find("Heart").GetComponent<Heart>();
        liver = GameObject.Find("Liver").GetComponent<Liver>();
        kidneys = GameObject.Find("Kidneys").GetComponent<Kidneys>();
        stomach = GameObject.Find("Stomach").GetComponent<Stomach>();
    }

    // Update is called once per frame
    void Update()
    {
        brainHealthBar.fillAmount = brain.health / brain.maxHealth;
        heartHealthBar.fillAmount = heart.health / heart.maxHealth;
        liverHealthBar.fillAmount = liver.health / liver.maxHealth;
        kidneysHealthBar.fillAmount = kidneys.health / kidneys.maxHealth;
        stomachHealthBar.fillAmount = stomach.health / stomach.maxHealth;
    }
}
