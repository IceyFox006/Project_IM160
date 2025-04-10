using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private GameObject _overworldPrefab;
    [SerializeField] private Vector3 _holdOffset;

    public string Name { get => _name; set => _name = value; }
    public Sprite IconSprite { get => _iconSprite; set => _iconSprite = value; }
    public GameObject OverworldPrefab { get => _overworldPrefab; set => _overworldPrefab = value; }
    public Vector3 HoldOffset { get => _holdOffset; set => _holdOffset = value; }
}
