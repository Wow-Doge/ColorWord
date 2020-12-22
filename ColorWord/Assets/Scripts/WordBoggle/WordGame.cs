#define EIGHT_DIRECTIONAL

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WordGame : MonoBehaviour {

	[HideInInspector]
	public Tile selectedTile;

	[HideInInspector]
	public List<Tile> selectedTiles;
	              
	public Text statusLabel;

	private Grid grid;

	private WordData wordData;

	public float score;
	public TextMeshProUGUI scoreLabel;
	public TextMeshProUGUI goalLabel;
	public TextMeshProUGUI time;

	[SerializeField] private float currentTime = 0f;
	[SerializeField] private float startingTime = 100f;

    private void Start()
    {
		Initialize();
    }

    private void Update()
    {
		CountingDownTime();
    }

	private void Initialize()
    {
		currentTime = startingTime;
		scoreLabel.text = score.ToString();
	}

	private void CountingDownTime()
    {
		currentTime -= 1 * Time.deltaTime;
		time.text = currentTime.ToString("0");
	}
    public void InitGame ()
	{
		grid = GetComponent<Grid> ();
		wordData = GetComponent<WordData> ();

		selectedTiles = new List<Tile> ();
		selectedTile = null;
		
		grid.BuildGrid ();
		statusLabel.text = "";
	}
	

	public void HandleTouchDown (Vector2 touch)
	{
		selectedTiles.Clear ();
		if (selectedTile != null)
			selectedTile.Select (false);


		selectedTile = TileCloseToPoint (touch);

		if (selectedTile != null) 
		{
			selectedTile.Select(true);
			if (!selectedTiles.Contains(selectedTile)) selectedTiles.Add (selectedTile);
		}
		statusLabel.text = (""+selectedTile.TypeChar).ToUpper();
	}

	public void HandleTouchUp (Vector2 touch)
	{
		if (selectedTile == null || selectedTiles.Count < 3)
			return;

		if (selectedTile != null) {
			selectedTile.Select(false);
			selectedTile = null;
		}

		char[] word = new char[selectedTiles.Count];
		for (var i = 0; i < selectedTiles.Count; i++) {
			var tile = selectedTiles[i];
			word[i] = tile.TypeChar;
			tile.Select(false);
		}

		var s = new string (word);
		statusLabel.text = s.ToUpper();

		if (wordData.IsValidWord (s)) 
		{
			for (var i = 0; i < selectedTiles.Count; i++) {
				var tile = selectedTiles [i];
				tile.gameObject.SetActive (false);
			}
			grid.CollapseGrid();
			AddScore();
		}
		selectedTiles.Clear ();
	}

	public void HandleTouchMove (Vector2 touch)
	{
		if (selectedTile == null)
			return;

		var nextTile = TileCloseToPoint (touch);

		if (nextTile != null && nextTile != selectedTile) 
		{

			if (nextTile.row == selectedTile.row && (nextTile.column == selectedTile.column - 1 || nextTile.column == selectedTile.column + 1))
			{
				selectedTile = nextTile;
			}
			else if (nextTile.column == selectedTile.column && (nextTile.row == selectedTile.row - 1 || nextTile.row == selectedTile.row + 1))
			{
				selectedTile = nextTile;
			}
			#if EIGHT_DIRECTIONAL
			else if (Mathf.Abs (nextTile.column - selectedTile.column) == 1 &&  Mathf.Abs (nextTile.row - selectedTile.row) == 1) 
			{
				selectedTile = nextTile;
			}
			#endif

			selectedTile.Select(true);
			if (!selectedTiles.Contains(selectedTile)) selectedTiles.Add (selectedTile);

			char[] word = new char[selectedTiles.Count];
			for (var i = 0; i < selectedTiles.Count; i++) {
				var tile = selectedTiles[i];
				word[i] = tile.TypeChar;
			}
			
			statusLabel.text = new string (word).ToUpper();
		}
	}

	private Tile TileCloseToPoint (Vector2 point)
	{
		var t = Camera.main.ScreenToWorldPoint (point);
		t.z = 0;

		int c = Mathf.FloorToInt ((t.x + grid.GRID_OFFSET_X + ( Tile.size * 0.5f )) / Tile.size);
		if (c < 0)
			c = 0;
		if (c >= grid.COLUMNS)
			c = grid.COLUMNS - 1;

		int r =  Mathf.FloorToInt ((grid.GRID_OFFSET_Y + ( Tile.size * 0.5f ) - t.y )/  Tile.size);
		if (r < 0) r = 0;
		if (r >= grid.ROWS) r = grid.ROWS - 1;

		return grid.gridTiles [c] [r];
	}

	public void AddScore()
    {
		score += 2;
		scoreLabel.text = score.ToString();
    }
}
