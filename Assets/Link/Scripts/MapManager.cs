using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class MapManager : MonoBehaviour
{
	public static MapManager _Instance; 
	[SerializeField] Block _itemPrefab;
	[SerializeField] List<Sprite> _sprites = new List<Sprite>();
	[SerializeField] int _width = 3;
	[SerializeField] int _height = 3;

	System.Random _random = new System.Random();

	void Awake()
	{
		_Instance = this; 
	}

	public void Init()
	{
		
	}

	public void GenerateMap()
	{
		for (int i = 0, count = _width * _height; i < count; ++i)
		{
			Block block = GameObject.Instantiate(_itemPrefab); 
			block._id = i; 
			block._pos = new Vector2(i % _width, i / _width); 
			block.transform.position = new Vector3(i % _width - _width / 2f + 0.5f, i / _width - _height / 2f + 0.5f, 0); 
			int index = _random.Next(0, _sprites.Count); 
			block._spriteIndex = index; 
			block._item.sprite = _sprites[index]; 
			block.name = "Item " + i.ToString(); 
			block._coordinateText.text = block.transform.position.x + ", " + block.transform.position.y; 
		}
	}

	void Start()
	{
		
	}

	void Update()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)); 
		if (Input.GetButtonDown("Fire1"))
		{
			Collider2D collider = Physics2D.OverlapPoint(pos, LayerMask.GetMask("Block")); 
			if (collider != null)
			{
				StartCoroutine(HideRoutine(collider.GetComponent<Block>()._mask.gameObject)); 
				Sprite sprite = collider.GetComponent<SpriteRenderer>().sprite; 
				if (_lastSelectedSprite == null)
				{
					_lastSelectedSprite = sprite; 
					_lastSelectedObj = collider.gameObject; 
					SpriteRenderer lastSprite = _lastSelectedObj.GetComponent<Block>()._mask.GetComponent<SpriteRenderer>(); 
					Color color = lastSprite.color; 
					color.r = 1; 
					lastSprite.color = color; 
					Debug.Log("last: " + _lastSelectedSprite.name); 
				}
				else
				{
					if (_lastSelectedSprite == sprite)
					{
						Debug.Log("Remove!!!"); 
						GameObject.Destroy(_lastSelectedObj); 
						GameObject.Destroy(collider.gameObject, 1f); 
					}
					else
					{
						SpriteRenderer lastSprite = _lastSelectedObj.GetComponent<Block>()._mask.GetComponent<SpriteRenderer>(); 
						Color color = lastSprite.color; 
						color.r = 0; 
						lastSprite.color = color; 
					}
					_lastSelectedSprite = null; 
				}
				Debug.Log("hit: " + collider.name); 
			}
		}
	}

	IEnumerator HideRoutine(GameObject obj)
	{
		obj.SetActive(false); 
		yield return new WaitForSeconds(0.5f); 
		obj.SetActive(true); 
	}


	#region Select

	Sprite _lastSelectedSprite; 
	GameObject _lastSelectedObj; 

	#endregion
}
