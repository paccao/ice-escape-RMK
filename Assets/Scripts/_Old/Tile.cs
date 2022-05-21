using UnityEngine;

public class OldTile : MonoBehaviour
{
	[SerializeField] private Color _baseColor, _offsetColor;
	[SerializeField] private SpriteRenderer _renderer;

	public void Init(bool isOffset)
	{
		_renderer.color = isOffset ? _offsetColor : _baseColor;
	}
}
