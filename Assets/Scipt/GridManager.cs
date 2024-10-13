using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int[,] board;
    private int[,] completed;
    private GameObject[,] prefabBoard;
    private (int, int)[] clicked;

    private float XMin = -1.5f;
    private float XMax = 1.5f;
    private float YMin = -3.6f;
    private float YMax = 0.78f;

    private float Xdiff, Ydiff;

    private int count = 0;
    public float matchDelay = 0.4f;
    public int types = 6;

    public int Row, Collumn;
    public GameObject jar;
    public GameObject Complete;
    public Sprite[] ingredient;

    /* 
     * 0 = empty
     * 1 = ingredient 1
     * 2 = ingredient 2
     * 3 = ingredient 3
     * 4 = ingredient 4
     * 5 = ingredient 5
     * 6 = ingredient 6
     * 7 = special 1
     * 8 = special 2
     * 16 = Delete
     */

    void Start()
    {
        //initialize 2d arrays
        board = new int[Row + 1, Collumn + 1];
        prefabBoard = new GameObject[Row + 1, Collumn + 1];
        completed = new int[Row + 1, Collumn + 1];
        clicked = new (int, int)[2];

        //calculate grid
        Xdiff = (XMax - XMin) / (Row - 1);
        Ydiff = (YMax - YMin) / (Collumn - 1);

        //Generate Board
        for (int i = 0; i < Collumn; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                board[i, j] = UnityEngine.Random.Range(1, types+1);
            }
        }


        //Check if board have match
        bool matched = true;

        while (matched == true)
        {
            CheckBoard(ref completed);
            matched = false;
            for (int i = 0; i < Collumn; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    if (completed[i, j] == 1)
                    {
                        board[i, j] = UnityEngine.Random.Range(1, types+1);
                        matched = true;
                    }
                }
            }
        }

        DrawBoard();
    }

    void DrawBoard()
    {
        for (int i = 0; i < Collumn; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                GameObject instance = Instantiate(jar);
                instance.transform.position = new Vector2(XMin + (Xdiff * j), YMin + (Ydiff * i));
                prefabBoard[i, j] = instance;
                instance.name = i.ToString() + j.ToString();
                if (board[i, j] < 16)
                {
                    prefabBoard[i, j].GetComponent<SpriteRenderer>().sprite = ingredient[board[i, j]];
                }
            }
        }
    }

    private void CheckBoard(ref int[,] completed)
    {
        completed = new int[Row + 1, Collumn + 1];
        for (int i = 0; i < Collumn; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                completed[i, j] = 0;
            }
        }

        for (int i = 0; i < Collumn; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                int k = board[i, j];
                if (k == 0) continue;
                for (int count = 0; k == board[i, j + count]; count++)
                {
                    if (count == 2)
                    {
                        completed[i, j] = 1;
                        completed[i, j + 1] = 1;
                        completed[i, j + 2] = 1;
                    }
                    if (count == 3) completed[i, j + 3] = 1;
                    if (count == 4) completed[i, j + 4] = 1;

                    if (j + count + 1 >= Row)
                    {
                        break;
                    }
                }
                for (int count = 0; k == board[i, j - count] && j - count >= 0; count++)
                {
                    if (count == 2)
                    {
                        completed[i, j] = 1;
                        completed[i, j - 1] = 1;
                        completed[i, j - 2] = 1;
                    }
                    if (count == 3) completed[i, j - 3] = 1;
                    if (count == 4) completed[i, j - 4] = 1;

                    if (j - count - 1 < 0)
                    {
                        break;
                    }
                }
                for (int count = 0; k == board[i + count, j] && i + count < Row; count++)
                {
                    if (count == 2)
                    {
                        completed[i, j] = 1;
                        completed[i + 1, j] = 1;
                        completed[i + 2, j] = 1;
                    }
                    if (count == 3) completed[i + 3, j] = 1;
                    if (count == 4) completed[i + 4, j] = 1;

                    if (i + count + 1 >= Collumn)
                    {
                        break;
                    }
                }
                for (int count = 0; k == board[i - count, j] && i - count >= 0; count++)
                {
                    if (count == 2)
                    {
                        completed[i, j] = 1;
                        completed[i - 1, j] = 1;
                        completed[i - 2, j] = 1;
                    }
                    if (count == 3) completed[i - 3, j] = 1;
                    if (count == 4) completed[i - 4, j] = 1;

                    if (i - count - 1 < 0)
                    {
                        break;
                    }
                }
            }
        }
    }

    void Swap(int _1x, int _1y, int _2x, int _2y)
    {
        int temp = board[_1x, _1y];
        board[_1x, _1y] = board[_2x, _2y];
        board[_2x, _2y] = temp;

        DrawTile(_1x, _1y);
        DrawTile(_2x, _2y);
    }

    void DrawTile(int x, int y)
    {
        Destroy(prefabBoard[x, y]);

        GameObject instance = Instantiate(jar);
        instance.transform.position = new Vector2(XMin + (Xdiff * y), YMin + (Ydiff * x));

        prefabBoard[x, y] = instance;
        instance.name = x.ToString() + y.ToString();
        if (board[x, y] < 16)
        {
            prefabBoard[x, y].GetComponent<SpriteRenderer>().sprite = ingredient[board[x, y]];
        }
    }

    private IEnumerator DeleteMatch(float sec)
    {
        bool matched2 = false;
        yield return new WaitForSeconds(sec);

        for (int i = Collumn - 1; i >= 0; i--)
        {
            for (int j = 0; j < Row; j++)
            {
                if (completed[i, j] == 1)
                {
                    board[i, j] = 0;
                    GameObject instance = Instantiate(Complete);
                    instance.transform.position = new Vector2(XMin + (Xdiff * j), YMin + (Ydiff * i));
                    matched2 = true;
                }
            }
        }

        if (matched2) StartCoroutine(DeleteMatch(sec));
    }

    private void PullDown(float sec)
    {
        StartCoroutine(DeleteMatch(sec));

        StartCoroutine(UpdateDown(sec));
    }

    private IEnumerator UpdateDown(float sec)
    {
        yield return new WaitForSeconds(sec);

        bool matched2 = false;

        for (int i = Collumn - 1; i >= 0; i--)
        {
            for (int j = 0; j < Row; j++)
            {
                if (completed[i, j] == 1 || board[i, j] == 0)
                {
                    if (i == Row)
                    {
                        board[i, j] = UnityEngine.Random.Range(1, types + 1);
                    }
                    else
                    {
                        board[i, j] = board[i + 1, j];
                        board[i + 1, j] = 0;
                        DrawTile(i + 1, j);
                    }
                    matched2 = true;
                    DrawTile(i, j);
                }
            }
        }
        CheckBoard(ref completed);

        if (matched2) PullDown(sec);
    }

    private void Update()
    {
        count = 0;

        //check click
        for (int i = 0; i < Collumn; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                if (prefabBoard[i, j].GetComponent<IngredientController>().active == 1)
                {
                    if (count < 2)
                    {
                        clicked[count] = (i, j);
                        count++;
                    }
                }
            }
        }

        //swap
        if (count >= 2)
        {
            int _1x = clicked[0].Item1;
            int _1y = clicked[0].Item2;
            int _2x = clicked[1].Item1;
            int _2y = clicked[1].Item2;
            //swap action
            if ((_1x == _2x && (_1y == _2y + 1 || _1y == _2y - 1)) || (_1y == _2y && (_1x == _2x + 1 || _1x == _2x - 1)))
            {
                Swap(_1x, _1y, _2x, _2y);

                CheckBoard(ref completed);

                bool matched = false;
                for (int i = 0; i < Collumn; i++)
                {
                    for (int j = 0; j < Row; j++)
                    {
                        if (completed[i, j] == 1)
                        {
                            matched = true;
                        }
                    }
                }

                PullDown(matchDelay);

                //swap back
                if (matched == false)
                {
                    Swap(_1x, _1y, _2x, _2y);
                }
            }
        }
    }
}
