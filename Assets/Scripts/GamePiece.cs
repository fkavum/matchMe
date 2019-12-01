using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchValue
{
    Yellow,
    Blue,
    Magenta,
    Indigo,
    Green,
    Teal,
    Red,
    Cyan,
    Wild,
    None
}

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;
    bool m_isMoving = false;
    Board m_board;

    public InterpType interpolation = InterpType.SmootherStep;
    public enum InterpType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep
    }

    public MatchValue matchValue;

 

    void Start()
    {
        
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move((int)transform.position.x + 2, (int)transform.position.y, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move((int)transform.position.x - 2, (int)transform.position.y, 0.5f);
        }*/
    }

    public void Init(Board board)
    {
        m_board = board;
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move(int destX,int destY,float timeToMove)
    {
        if (!m_isMoving) { 
        StartCoroutine(MoveRoutine(new Vector3(destX,destY,0),timeToMove));
        }
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;
        bool reachedDestination = false;
        float elapsedTime = 0f;
        m_isMoving = true;
        while (!reachedDestination)
        {
            if(Vector3.Distance(transform.position,destination) < 0.01f)
            {
                reachedDestination = true;
                if(m_board != null)
                {
                    m_board.PlaceGamePiece(this, (int)destination.x, (int)destination.y);
                }
                break;
            }

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / timeToMove,0f,1f);

            
            switch (interpolation)
            {
                case InterpType.Linear:
                    break;
                case InterpType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f); // Ease out  2.10
                    break;
                case InterpType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);  // Ease in
                    break;
                case InterpType.SmoothStep:
                    t = t * t * (3 - 2 * t);  //SmoothStep  (both ease in and ease out)
                    break;
                case InterpType.SmootherStep:
                    t = t * t * t * (t * (t * 6 - 15) + 10); //SmootherStep
                    break;
                default:
                    break;
            }

            t = Mathf.Clamp(t, 0f, 1f);
            transform.position = Vector3.Lerp(startPosition, destination, t);



            yield return null;
        }

        m_isMoving = false;

    }

    public void ChangeColor(GamePiece pieceToMatch)
    {
        SpriteRenderer rendererToChange = GetComponent<SpriteRenderer>();
        Color colorToMatch = Color.clear;

        if(pieceToMatch != null)
        {
            SpriteRenderer rendererToMatch = pieceToMatch.GetComponent<SpriteRenderer>();
            if(rendererToMatch != null && rendererToChange != null)
            {
                rendererToChange.color = rendererToMatch.color;
            }

            matchValue = pieceToMatch.matchValue;
        }

    }


}
