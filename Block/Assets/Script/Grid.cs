using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();

    void Start(){
        CreateGrid();
    }

    private void CreateGrid(){
        SpawnGridSquares();
        SetGridSqauresPosition();
    }
    
    private void SpawnGridSquares(){
        // 0, 1, 2, 3, 4
        // 5, 6, 7, 8, 9

        int sqaureIndex = 0;
        for (var row = 0; row < rows; ++ row){
            for (var column = 0; column < columns; ++ column){
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SetImage(sqaureIndex % 2 == 0);
                sqaureIndex++;
            }
        }
    }
    
    private void SetGridSqauresPosition(){
        int columnNumber = 0;
        int rowNumber = 0;
        Vector2 squareGapNumber = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;

        var squareRect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = squareRect.rect.width * squareRect.transform.localScale.x + everySquareOffset;
        _offset.y = squareRect.rect.height * squareRect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquares){
            if(columnNumber + 1 > columns){
                squareGapNumber.x = 0;
                columnNumber = 0;
                rowNumber ++;
                rowMoved = true;
            }

            var posOffsetX = _offset.x * columnNumber + (squareGapNumber.x * squaresGap);
            var posOffsetY = _offset.y * rowNumber + (squareGapNumber.y * squaresGap);

            if(columnNumber > 0 && columnNumber % 3 == 0){
                squareGapNumber.x ++;
                posOffsetX += squaresGap;
            }
            if(rowNumber > 0 && rowNumber % 3 == 0 && rowMoved == false){
                rowMoved = true;
                squareGapNumber.y ++;
                posOffsetY += squaresGap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + posOffsetX, startPosition.y - posOffsetY);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + posOffsetX, startPosition.y - posOffsetY, 0.0f);

            columnNumber ++;
        }
    }
}