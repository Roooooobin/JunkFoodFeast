//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = System.Random;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 100;

    private GameObject _gazedAtObject = null;

    // necessary preprocessing data structures
    public static Process process;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    ///
    public void Start()
    {
        process = new Process();
    }

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }
}


public class Generator
{
    private Random _tableRandom;
    private Random _foodRandom;
    private const int MaxComboNum = 5; // at most 5 foods in one combo
    private const int TableNum = 4;
    private const int FoodNum = 10;

    public Generator()
    {
        _tableRandom = new Random();
        _foodRandom = new Random();
    }

    public int GetRandomTable()
    {
        return _tableRandom.Next(0, TableNum);
    }

    public ArrayList GetRandomFoods()
    {
        Random countRandom = new Random();
        int comboNum = countRandom.Next(0, MaxComboNum);
        ArrayList pack = new ArrayList();
        for (int i = 0; i <= comboNum; i++)
        {
            pack.Add(_foodRandom.Next(0, FoodNum));
        }

        return pack;
    }
}

public class Process
{
    private Generator _generator;
    private ArrayList _table;
    private ArrayList _plate;
    private string[] _foods;
    private int _status;
    private int _score;
    private int _time;

    public int StatusWaitToStart = 0;
    public int StatusSucceed = 1;
    public int StatusFail = 2;
    public int StatusInProcess = 3;

    public void DistributeFood()
    {
        _table = _generator.GetRandomFoods();
    }

    public Process()
    {
        _foods = new[]
        {
            "Lamb_chop", "Salami", "Lamb_leg", "HotDog", "Frappe", "Pretzel",
            "Soda_can", "Cake", "Doughnut", "Pizza",
        };
        _generator = new Generator();
        _table = new ArrayList();
        _plate = new ArrayList();
        DistributeFood();
        _status = StatusWaitToStart;
        // initial score is 0
        _score = 0;
        //
        _time = 60;
    }

    public ArrayList GetTable()
    {
        return _table;
    }

    public ArrayList GetPlate()
    {
        return _plate;
    }

    public void ClearPlate()
    {
        _plate.Clear();
    }

    public string[] GetFoods()
    {
        return _foods;
    }

    public void AddFood(int foodIndex)
    {
        _plate.Add(foodIndex);
    }

    public int GetStatus()
    {
        return _status;
    }

    public void SetStatus(int status)
    {
        _status = status;
    }

    public int GetScore()
    {
        return _score;
    }

    public void UpdateScore()
    {
        _score++;
    }

    public string FormComboString(ArrayList foods)
    {
        var foodCount = new Dictionary<int, int>();
        foreach (int x in foods)
        {
            if (foodCount.ContainsKey(x))
            {
                foodCount[x]++;
            }
            else
            {
                foodCount[x] = 1;
            }
        }

        string comboString = "";
        foreach (KeyValuePair<int, int> entry in foodCount)
        {
            comboString += $"{_foods[entry.Key]} x {entry.Value},";
        }

        return comboString.TrimEnd(',');
    }

    public bool CompareAndServe()
    {
        if (_table.Count != _plate.Count)
        {
            return false;
        }

        _table.Sort();
        _plate.Sort();
        for (int i = 0; i < _table.Count; i++)
        {
            if (!_table[i].Equals(_plate[i]))
            {
                return false;
            }
        }
        // is the same, continue to serve 
        // generate the next table to serve
        DistributeFood();
        return true;
    }
}