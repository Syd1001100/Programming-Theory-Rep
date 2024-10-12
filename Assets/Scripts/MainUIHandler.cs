using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crop // ABSTRACTION
{
    // Info with ENCAPSULATION
    public string Name { get; protected set; }
    public float Health { get; protected set; }
    public bool IsReadyToHarvest => Health >= 100f;
    public Material[] GrowthMaterials;
    private int growthStage;
    public float GrowthRate { get; protected set; } = 1f; // Health increase per second

    // Constructor
    protected Crop(string name, Material[] materials)
    {
        Name = name;
        GrowthMaterials = materials;
        Health = 0f;
        growthStage = 0;
    }

    public virtual void Water()
    {
        Health += 10f; // Increase health when watered
        UpdateMaterial();
    }

    public virtual void Fertilize()
    {
        Health += 20f;
        UpdateMaterial();
    }

    public abstract void Plant();
    public virtual float Harvest()
    {
        if (IsReadyToHarvest) {
            float yield = 2f;
            Health = 0f;
            growthStage = 0;
            return yield;
        }
        return 0f;
    }

    protected void UpdateMaterial()
    {
        if (growthStage < GrowthMaterials.Length - 1) 
        {
            growthStage = Mathf.FloorToInt(Health / 9.09f);
        }
    }

    public Material GetCurrentMaterial()
    {
        return GrowthMaterials[growthStage];
    }

    public void Grow(float deltaTime)
    {
        Health += GrowthRate * deltaTime;
        UpdateMaterial();
    }
}

public class Wheat : Crop // INHERITANCE
{
    public Wheat(Material[] materials) : base("Wheat", materials) { }

    public override void Plant() // POLYMORPHISM
    {
        Health = 0f; // Health resets when planted
    }
}

public class Corn : Crop // INHERITANCE
{
    public Corn(Material[] materials) : base("Corn", materials) { }

    public override void Plant() // POLYMORPHISM
    {
        Health = 0f; // Health resets when planted
    }
}