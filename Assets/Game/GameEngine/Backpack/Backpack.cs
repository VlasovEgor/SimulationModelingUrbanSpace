using System.Collections.Generic;
using UnityEngine;

public class Backpack
{
    private List<Item> _bestItems = new();

    private int _maxWeight;

    private int _bestPrice;

    public Backpack(int weight)
    {
        _maxWeight = weight;
    }

    private int CalcWeigth(List<Item> items)
    {
        int sumW = 0;

        foreach (Item item in items)
        {   
            sumW += item.Weigth;
        }

        return sumW;
    }

    private int CalcPrice(List<Item> items)
    {
        int sumPrice = 0;

        foreach (Item item in items)
        {
            sumPrice += item.Price;
        }

        return sumPrice;
    }

    private void CheckSet(List<Item> items)
    {
        if (_bestItems.Count == 0)
        {
            if (CalcWeigth(items) <= _maxWeight)
            {
                _bestItems = items;
                _bestPrice = CalcPrice(items);
            }
        }
        else
        {
            if (CalcWeigth(items) <= _maxWeight && CalcPrice(items) > _bestPrice)
            {
                _bestItems = items;
                _bestPrice = CalcPrice(items);
            }
        }
    }

    public void MakeAllSets(List<Item> items)
    {
        if (items.Count > 0)
            CheckSet(items);

        for (int i = 0; i < items.Count; i++)
        {
            List<Item> newSet = new List<Item>(items);

            newSet.RemoveAt(i);

            MakeAllSets(newSet);
        }

    }

    public List<Item> GetBestSet()
    {
        return _bestItems;
    }
}

public class Item
{
    public BuildingConfig Config { get; set; }

    public int Weigth { get; set; }

    public int Price { get; set; }

    public Item(BuildingConfig config, int weigth, int price)
    {
        Config = config;
        Weigth = weigth;
        Price = price;
    }
}
