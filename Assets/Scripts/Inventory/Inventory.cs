﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Inventory found!");
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (items.Count >= space) {
            return false;
        }

        items.Add(item);
        SubscribeChange();

        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);
        SubscribeChange();
    }

    public void SubscribeChange() {
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

}
