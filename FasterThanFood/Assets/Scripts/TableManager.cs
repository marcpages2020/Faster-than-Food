using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TableManager : MonoBehaviour
{
    [SerializeField] Table[] tables;
    private List<Table> freeTables;
    private List<Table> occupiedTables;

    private void Awake()
    {
        freeTables = new List<Table>();
        occupiedTables = new List<Table>();

        for(int i = 0; i < tables.Length; i++)
        {
            freeTables.Add(tables[i]);
        }
    }

    public Table GetRandomTable()
    {
        if (freeTables.Count > 0) 
        { 
            int index = Random.Range(0, freeTables.Count -1);
            return freeTables[index];
        }
        else
        {
            return null;
        }
    }

    public Table GetClosestFreeTable(Vector3 position)
    {

        Table closestFreeTable = null;
        float closestDistance = 10000;

        for (int i = 0; i < freeTables.Count; i++)
        {
            if (closestFreeTable == null)
            {
                closestFreeTable = freeTables[i];
                closestDistance = Vector3.Distance(closestFreeTable.transform.position, position);
            }
            else
            {
                float distance = Vector3.Distance(freeTables[i].transform.position, position);

                if (distance < closestDistance)
                {
                    closestFreeTable = freeTables[i];
                    closestDistance = distance;
                }
            }
        }

        return closestFreeTable;
    }

    public bool OccupyTable(Table table, Client client)
    {
        foreach (Table t in freeTables)
        {
            if (t == table)
            {
                table.OccupyTable(client);
                freeTables.Remove(table);
                occupiedTables.Add(table);
                return true;
            }
        }
        return false;
    }

    public bool FreeTable(Table table)
    {
        foreach (Table t in occupiedTables)
        {
            if (t == table)
            {
                table.FreeTable();
                occupiedTables.Remove(table);
                freeTables.Add(table);
                return true;
            }
        }
        return false;
    }
}
