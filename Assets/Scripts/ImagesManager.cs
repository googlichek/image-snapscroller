using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImagesManager : MonoBehaviour
{
	public Image ImagePrefab;

	private string _path;

	private void Awake()
	{
		_path = Application.streamingAssetsPath;
		InitiateImageCreation();
	}

	private void InitiateImageCreation()
	{
		string[] fileNames = Directory.GetFiles(_path, "*.png").ToArray();

		for (int i = 0; i < fileNames.Length; i++)
		{
			StartCoroutine(CreateImage(fileNames[i], i));
		}
	}

	private IEnumerator CreateImage(string url, int index)
	{
		WWW www = new WWW("file://" + url);
		yield return www;

		Image image = Instantiate(ImagePrefab, transform);

		Texture texture = www.texture;
		Sprite loadedSprite =
			Sprite.Create(
				(Texture2D) texture,
				new Rect(0, 0, texture.width, texture.height),
				Vector2.zero);

		Vector2 currentPosition = new Vector2(1920*index, 0);

		image.sprite = loadedSprite;
		image.transform.localPosition = currentPosition;
	}
}
