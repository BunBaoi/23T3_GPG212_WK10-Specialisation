using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int HealthBar = 100;
    [SerializeField] int WaterBar = 100;
    [SerializeField] int FoodBar = 100;
    int num = 0;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    void HealthCheck()
    {
        if (FoodBar <= num)
        {
            if (WaterBar <= num)
            {
                HealthBar -= 1;
            }
        }
    }

    void Eating()
    {
        if(Input.GetKey(KeyCode.Mouse1) && gameObject.tag == "FoodItem")
        {
            // Incorporate Food stats from their own scripts
            //FoodBar = FoodBar += FoodItemStats;
        }
    }
}
