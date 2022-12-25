using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Container : MonoBehaviour
{
    // Items available in Container
    private class ContainerItem
    {
        public System.Guid Id;
        public string Name;
        public int Maxmium;

        private int amountTaken;


        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }

        public int Remaining
        {
            get
            {
                return Maxmium - amountTaken;
            }
        }

        public int Get(int value)
        {
            if(amountTaken + value > Maxmium)
            {
                int toMuch = (amountTaken + value) - Maxmium;
                amountTaken = Maxmium;
                return value - toMuch;
            }
            amountTaken += value;
            return value;
        }

    }

    List<ContainerItem> items;

    private void Awake()
    {
        items = new List<ContainerItem>();
    }

    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            Maxmium = maximum,
            Name = name
        });
        return items.Last().Id;
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Get(amount);
    }

    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Remaining;
    }
    private ContainerItem GetContainerItem(System.Guid id)
    {
        var containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        return containerItem;
    }

}
