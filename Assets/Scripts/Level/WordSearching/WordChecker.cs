using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordChecker : MonoBehaviour
{
    [Header("audioManager")]
    public SoundManager audiomanager;

    //timemanger
    public timeManager timer;
    public progressbarTimeWordScramble progressbar;//script progresbarnya sama dengan word scramble jadi ku pake
    //puzzle manager
    public PuzzleManager pzm;

    public GameData currentGameData;

    private string _word;

    private int _assignPoints = 0;
    private int _complateWords = 0;
    private Ray rayUp, rayDown;
    private Ray rayLeft, rayRight;
    private Ray rayDiagonalLeftUp, RayDiagonalLeftDown;
    private Ray rayDiagonalRightUp, RayDiagonalRightDown;
    private Vector3 _rayStartPosition;
    private Ray currentRay = new Ray();
    private List<int> correctList = new List<int>();

    public int x;
    public bool cek;
    public bool solved;

    // Bar Player-------------------------
    public player playermanager;
    public GameObject progresBarPlayer;


    //line test
    public Vector3 starline;
    public Vector3 endline;

    private void OnEnable()
    {
        GameEvent.OnChecksquare += SquareSelected;
        GameEvent.OnClearSelect += clearslection;
    }
    private void OnDisable()
    {
        GameEvent.OnChecksquare -= SquareSelected;
        GameEvent.OnClearSelect += clearslection;
    }
    private void Awake()
    {
        //audio
        audiomanager = FindObjectOfType<SoundManager>();

        pzm.jumlahSoal += 1;
        x = currentGameData.selectBoardData.searchingWords.Count;
        solved = false;
    }
    void Start()
    {
        // Bar Player
        progresBarPlayer = FindInActiveObjectByTag("BarPlayer");
        progresBarPlayer.SetActive(true);
        playermanager = FindObjectOfType<player>();
        //-----------------------------------------------------
        timer.waktu = timer.menit * 60;
        progressbar.maxlenghtTime = timer.waktu;
        progressbar.currentTime = timer.waktu;
        _assignPoints = 0;
        _complateWords = 0;
    }

    void Update()
    {
        CheckBoard();
        if (solved == false)
        {
            //barPlayer.current = 0; //Bar Player

            timer.waktu -= Time.deltaTime;
            progressbar.currentTime = timer.waktu;
        }
        if (timer.waktu <= 0.1f)
        {
            audiomanager.popupMetohod(4);
            Destroy(this.gameObject);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (cek == true)
            {
                audiomanager.popupMetohod(3);
                timer.waktu -= 5f;
                cek = false;
            }
            if (solved)
            {
                audiomanager.popupMetohod(2);
                pzm.score += 1;
                playermanager.progresplayer[playermanager.nourut].current++; // Bar Player
                Destroy(this.gameObject);
            }
        }


        if (_assignPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(rayUp.origin, rayUp.direction * 4);
            Debug.DrawRay(rayDown.origin, rayDown.direction * 4);
            Debug.DrawRay(rayLeft.origin, rayLeft.direction * 4);
            Debug.DrawRay(rayRight.origin, rayRight.direction * 4);
            Debug.DrawRay(rayDiagonalLeftUp.origin, rayDiagonalLeftUp.direction * 4);
            Debug.DrawRay(RayDiagonalLeftDown.origin, RayDiagonalLeftDown.direction * 4);
            Debug.DrawRay(rayDiagonalRightUp.origin, rayDiagonalRightUp.direction * 4);
            Debug.DrawRay(RayDiagonalRightDown.origin, RayDiagonalRightDown.direction * 4);
        }
    }
    private void SquareSelected(string Letter, Vector3 squareposition, int squareIndex)
    {
        if (_assignPoints == 0)
        {
            _rayStartPosition = squareposition;
            correctList.Add(squareIndex);
            _word += Letter;

            rayUp = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(0, 1));
            rayDown = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(0, -1));
            rayLeft = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(-1, 0));
            rayRight = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(1, 0));
            rayDiagonalLeftUp = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(-1, 1));
            RayDiagonalLeftDown = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(-1, -1));
            rayDiagonalRightUp = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(1, 1));
            RayDiagonalRightDown = new Ray(new Vector2(squareposition.x, squareposition.y), new Vector2(1, -1));
        }
        else if (_assignPoints == 1)
        {
            correctList.Add(squareIndex);
            currentRay = SelectRay(_rayStartPosition, squareposition);
            GameEvent.selectSquareMethod(squareposition);
            _word += Letter;
            checkWord();
        }
        else
        {
            if (IsPointOnTheRay(currentRay, squareposition))
            {
                correctList.Add(squareIndex);
                GameEvent.selectSquareMethod(squareposition);
                _word += Letter;
                checkWord();
            }
        }
        _assignPoints++;
    }
    private void checkWord()
    {
        foreach (var serchingWord in currentGameData.selectBoardData.searchingWords)
        {
            if (_word == serchingWord.word)
            {
                GameEvent.correctWordMethod(_word, correctList);
                x -= 1;
                //DrawLine(starline, endline, Color.red);
                audiomanager.popupMetohod(2);
                _word = string.Empty;
                cek = false;
                return;
            }
            else
            {
                audiomanager.slidingPuzzleMetohod(0);
                cek = true;
            }
        }
    }

    private bool IsPointOnTheRay(Ray currentRay, Vector3 point)
    {

        var hits = Physics.RaycastAll(currentRay, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.position == point)
            {
                //starline.position = point;
                return true;
            }
        }
        return false;
    }
    private Ray SelectRay(Vector2 firstPosition, Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.01f;
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - 1f) < tolerance)
        {
            return rayUp;
        }
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y + 1f) < tolerance)
        {
            return rayDown;
        }
        if (Mathf.Abs(direction.x + 1f) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return rayLeft;
        }
        if (Mathf.Abs(direction.x - 1f) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return rayRight;
        }
        if (direction.x < 0f && direction.y > 0f)
        {
            return rayDiagonalLeftUp;
        }
        if (direction.x < 0f && direction.y < 0f)
        {
            return RayDiagonalLeftDown;
        }
        if (direction.x > 0f && direction.y < 0f)
        {
            return RayDiagonalRightDown;
        }
        if (direction.x > 0f && direction.y > 0f)
        {
            return rayDiagonalRightUp;
        }

        return rayDown;
    }
    private void clearslection()
    {
        _assignPoints = 0;
        correctList.Clear();

        _word = string.Empty;
    }
    private void CheckBoard()
    {
        if (x == _complateWords)
        {
            print("sukses");
            solved = true;
        }
    }

    GameObject FindInActiveObjectByTag(string tag) //fungsi mencari object yang tidak aktif menggunakan tag
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
    //mengambar garis di unity
    public LineRenderer lR;

    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        //GameObject.Destroy(myLine, duration);
    }
}
