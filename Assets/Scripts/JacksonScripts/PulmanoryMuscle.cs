// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PulmanoryMuscle : Organ
// {
//     public GameObject boosters;
//     public GameObject deactivatedBoosters;

//     private bool dying = false;
    
//     protected override void HealthEffects()
//     {
//         if (health < 30 && !dying)
//         {
//             dying = true;
//             Invoke("PulmanoryDying", boostDeleteRate);
//         }
//         else if (health >= 30 && dying)
//         {
//             dying = false;
//             Invoke("GenerateBoosts", boostDeleteRate);
//         }
//     }

//     private void PulmanoryDying()
//     {
//         // Remove an boost every 5 seconds
//         foreach (Transform a in boostInstances.Keys)
//         {
//             Destroy(boostInstances[a]);
//             boostInstances.Remove(a);
//             break;
//         }
//         // Continue dying if dying
//         if (dying)
//         {
//             Invoke("PulmanoryDying", boostDeleteRate);
//         }
//     }

//     private void GenerateBoosts()
//     {
//         foreach (Transform boost in boostPositions.transform)
//         {
//             // Generate a speed booster in some location that doesn't already contain one
//             if (!boostInstances.ContainsKey(boost))
//             {
//                 GameObject booster = Instantiate(boostPrefab, boost.transform.position, boost.transform.rotation);
//                 boostInstances.Add(boost, booster);
//             }
//         }
//     }
// }
