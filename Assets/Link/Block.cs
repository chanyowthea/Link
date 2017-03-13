using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using System; 

public class Block : MonoBehaviour
{
	[SerializeField] public SpriteRenderer _mask; 
	[SerializeField] public SpriteRenderer _item; 
	[SerializeField] public Text _coordinateText; 
	[NonSerialized] public int _spriteIndex;  
	[NonSerialized] public int _id; 
	[NonSerialized] public Vector2 _pos; 
}
