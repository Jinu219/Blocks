using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shape : MonoBehaviour {
    public GameObject squareShapeImage;

    // [HideInInspector]
    public ShapeData currentShapeData;
    private List<GameObject> _currentShape = new List<GameObject>();

    void Start(){
        RequestNewShape(currentShapeData);
    }
    
    public void RequestNewShape(ShapeData shapeData){
        CreateShapes(shapeData);
    }

    public void CreateShapes(ShapeData shapeData){
        currentShapeData = shapeData;
        var totalSqaureNumber = GetNumberOfSquares(shapeData);

        while(_currentShape.Count <= totalSqaureNumber){
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }

        foreach(var square in _currentShape){
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;
        for(var row = 0; row < shapeData.rows; row++){
            for(var column = 0; column < shapeData.columns; column++){
                if(shapeData.board[row].column[column]){
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), GetYPositionForShapeSquare(shapeData, row, moveDistance));
                    currentIndexInList++;
                }
            }
        }
    }
    public float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance){
        float shiftOnY = 0f;
        
        if(shapeData.rows > 1){
            if(shapeData.rows % 2 != 0){
                var middleSquareIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows -1) / 2;
                if(row < middleSquareIndex){
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if(row > middleSquareIndex){
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
            else{
                var middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2); 
                var middleSquareIndex1 = (shapeData.rows == 2) ? 0 : shapeData.rows -2;
                var multiplier = shapeData.rows / 2;

                if(row == middleSquareIndex1 || row == middleSquareIndex2){
                    if(row == middleSquareIndex2) shiftOnY = (moveDistance.y / 2) * -1;
                    if(row == middleSquareIndex1) shiftOnY = (moveDistance.y / 2);
                }
                if(row < middleSquareIndex1 && row < middleSquareIndex2){
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if(row > middleSquareIndex1 && row > middleSquareIndex2){
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }

    public float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance){
        float shiftOnX = 0f;

        if(shapeData.columns > 1){
            if(shapeData.columns % 2 != 0){
                var middleSquareIndex = (shapeData.columns -1 ) / 2;
                var multiplier = (shapeData.columns -1 ) / 2;
                if(column < middleSquareIndex){
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if(column > middleSquareIndex){
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
            else{
                var middleSquareIndex2 = (shapeData.columns == 2) ? 1 : (shapeData.columns / 2);
                var middleSquareIndex1 = (shapeData.columns == 2) ? 0 : (shapeData.columns - 1);
                var multiplier = shapeData.columns / 2;

                if(column == middleSquareIndex1 && column < middleSquareIndex2){
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if(column > middleSquareIndex1 && column > middleSquareIndex2){
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }

            }
        }
        return shiftOnX;
    }

    private int GetNumberOfSquares(ShapeData shapeData){
        int number = 0;
        foreach(var rowData in shapeData.board){
            foreach(var active in rowData.column){
                if(active) number++;
            }
        }
        return number;
    }
}
