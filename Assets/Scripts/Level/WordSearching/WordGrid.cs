using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGrid : MonoBehaviour
{
  
    public GameData currentGameData;
    public GameObject _gridSquarePrefab;
    public alfabetData AlfabetData;
    public GridSquare _gridsquarescript;

    public timeManager timer;

    public float squareOffset = 0.0f;
    public float topPosition;
    public Transform posisi;

    private List<GameObject> _squareList = new List<GameObject>();

    private void Awake()
    {
        timer.GetComponent<timeManager>();
        _gridsquarescript.timer= timer;
    }
    void Start()
    {
        spawnGridSquare();
        setSquarePosition();
    }
    
    private void setSquarePosition()
    {
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = _squareList[0].GetComponent<Transform>();

        var offset = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f

        };
        var startPosition = GetFirsPosition();
        int columnNumber = 0;
        int RowNumber = 0;

        foreach (var square in _squareList)
        {
            if (RowNumber + 1 > currentGameData.selectBoardData.Rows)
            {
                columnNumber++;
                RowNumber = 0;
            }
            var positionX = startPosition.x + offset.x * columnNumber;
            var positionY = startPosition.y - offset.y * RowNumber;

            square.GetComponent<Transform>().position = new Vector2(positionX, positionY);
            RowNumber++;

        }

    }
    private Vector2 GetFirsPosition()
    {
        var startposition = new Vector2(0, transform.position.y);
        var squareRect = _squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransofrm = _squareList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.width * squareTransofrm.localScale.x;
        squareSize.y = squareRect.height * squareTransofrm.localScale.y;

        //var midwidthPositon = (((currentGameData.selectBoardData.Columns - 1) * squareSize.x) / 200) * 0.01f;
        var midwidthPositon = posisi.localPosition;
        var midwidthHegiht = (((currentGameData.selectBoardData.Rows - 1) * squareSize.y) / 12) * 0.01f;

        //startposition.x = (midwidthPositon != 0) ? midwidthPositon * -1 : midwidthPositon;
        startposition = midwidthPositon / 2;
        startposition.y += midwidthHegiht;

        return startposition;
    }

    private void spawnGridSquare()
    {
        if (currentGameData != null)
        {
            var squareScale = GetSquareScale(new Vector3(1.5f, 1.5f, 0.5f));
            foreach (var square in currentGameData.selectBoardData.Board)
            {
                foreach (var squareLetter in square.Row)
                {
                    var normalletterData = AlfabetData.alfabetnormal.Find(data => data.letter == squareLetter);
                    var selectedLetterData = AlfabetData.alfabetHighlighted.Find(data => data.letter == squareLetter);
                    var currentLetterData = AlfabetData.alfabetwrong.Find(data => data.letter == squareLetter);

                    if (normalletterData.image == null || selectedLetterData.image == null)
                    {
                        Debug.LogError("semua field dalam array sebaiknya memiliki sebuah tulisan. tekan tombol fill up random pada board anda untuk menambahkan tulisan random. tulisan: " + squareLetter);

#if UNITY_EDITOR

                        if (UnityEditor.EditorApplication.isPlaying)
                        {
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
#endif
                    }
                    else
                    {
                        _squareList.Add(Instantiate(_gridSquarePrefab));
                        _squareList[_squareList.Count - 1].GetComponent<GridSquare>().setSprites(normalletterData, currentLetterData, selectedLetterData);
                        _squareList[_squareList.Count - 1].transform.SetParent(this.transform);
                        _squareList[_squareList.Count - 1].GetComponent<Transform>().position = new Vector3(0, 0, 0);
                        _squareList[_squareList.Count - 1].transform.localScale = squareScale;
                        _squareList[_squareList.Count - 1].GetComponent<GridSquare>().setIndex(_squareList.Count - 1);
                    }
                }
            }
        }
    }
    private Vector3 GetSquareScale(Vector3 defaultscale)
    {
        var finalScale = defaultscale;
        var adjustment = 0.01f;
        while (ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if (finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;
            }
        }
        return finalScale;
    }
    private bool ShouldScaleDown(Vector3 targetscale)
    {
        var squareRect = _gridSquarePrefab.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0, 0);
        var startPosition = new Vector2(0, 0);

        squareSize.x = (squareRect.width * targetscale.x) + squareOffset;
        squareSize.y = (squareRect.height * targetscale.y)+ squareOffset;

        var midWidthPosition = ((currentGameData.selectBoardData.Columns * squareSize.x) / 2) * 0.01f;
        var midWidthHight = ((currentGameData.selectBoardData.Rows * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -4 : midWidthPosition;
        startPosition.y = midWidthHight;

        return (startPosition.x < GetHalfScreenWidth() * -1 || startPosition.y > topPosition);
    }
    private float GetHalfScreenWidth()
    {
        float height = Camera.main.orthographicSize * 2;
        //float height = posisi.localScale.x * 2;
        float width = (1.4f * height) * Screen.width / Screen.height;
        //float width = (1.4f * height) * posisi.localScale.y / posisi.localScale.x;
        return width / 2;
    }
}
