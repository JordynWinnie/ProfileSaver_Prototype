﻿using UnityEngine;
using UnityEngine.UI;

public class FlexibleLayoutGroup : LayoutGroup
{
    public enum FitType { Fixed, Width, Height }

    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public FitType fitType;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrRt = Mathf.Sqrt(transform.childCount);

        rows = Mathf.CeilToInt(sqrRt);
        columns = Mathf.CeilToInt(sqrRt);

        switch (fitType)
        {
            case FitType.Width:
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
                break;

            case FitType.Height:
                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
                break;

            default:
                break;
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)columns) - (spacing.x / (float)columns * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = (parentHeight / (float)rows) - (spacing.y / (float)rows * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowCount = i / columns;
            int colCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * colCount) + (spacing.x * colCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}