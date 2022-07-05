using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("audioManager")]
    public SoundManager audiomanager;

    public static QuizManager instance; //Instance to make is available in other scripts without reference

    [SerializeField] private GameObject gameComplete;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataScriptable questionDataScriptable;
    [SerializeField] private WordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private WordData[] optionsWordList;    //list of options word in the game
    [SerializeField] private Text[] answerWordLisText;
    [SerializeField] private Text[] answerWordLisTextDefault;

    private GameStatus gameStatus = GameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[8];               //array which store char of each options

    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question
    //tambahan ku
    private bool timeCheck;
    public timeManager time;
    public progressbarTimeWordScramble progressbar;
    public bool solved;

    public PuzzleManager pzm;
    public player playermanager;
    private int keluarke = 0;
    private void Awake()
    {
        //audio
        audiomanager = FindObjectOfType<SoundManager>();

        // Bar Player
        playermanager = FindObjectOfType<player>();

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pzm.jumlahSoal += 1;
        solved = false;
        timeCheck = true;
        time.waktu = time.menit * 60;
        progressbar.maxlenghtTime = time.waktu;
        progressbar.currentTime = time.waktu;
        if (time.waktu > 0.1)
        {
            selectedWordsIndex = new List<int>();           //create a new list at start
            SetQuestion();                                  //set question
        }
    }
    private void Update()
    {
        if (solved == false)
        {
            answerWordList[currentAnswerIndex /*+ keluarke*/].SetWord(answerWord[currentAnswerIndex /*+ keluarke*/]);
        }
        if (timeCheck == true)
        {
            time.waktu -= Time.deltaTime;
            progressbar.currentTime = time.waktu;
        }
        if (time.waktu <= 0.1f)
        {
            audiomanager.popupMetohod(4);
            Destroy(this.gameObject);
            solved = false;
        }
    }
    void SetQuestion()
    {
        gameStatus = GameStatus.Playing;                //set GameStatus to playing 

        //set the answerWord string variable
        answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;

        ResetQuestion();                               //reset the answers and options value to orignal

        selectedWordsIndex.Clear();                     //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array

        //add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {
            wordsArray[i] = char.ToUpper(answerWord[i]);
            answerWordLisTextDefault[i].color = new Color(answerWordLisTextDefault[i].color.r, answerWordLisTextDefault[i].color.g, answerWordLisTextDefault[i].color.b, 0.5f);
        }

        //add the dummy char to wordsArray
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
        }

        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array

        //set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }

    }
    public void resetbtn()
    {
        audiomanager.buttonclickMethod();
    }
    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
        keluarke = 0;
        //activate all the answerWordList gameobject and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < answerWord.Length; i++)
        {

            answerWordList[i].SetWord('_');

            answerWordLisTextDefault[i].color = new Color(answerWordLisTextDefault[i].color.r, answerWordLisTextDefault[i].color.g, answerWordLisTextDefault[i].color.b, 0.5f);
        }

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(false);
        }

        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
        }

        currentAnswerIndex = 0;
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(WordData value)
    {
        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;
        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list
        answerWordLisText[currentAnswerIndex].color = new Color(answerWordLisText[currentAnswerIndex].color.r, answerWordLisText[currentAnswerIndex].color.g, answerWordLisText[currentAnswerIndex].color.b, 1);

        //audio
        audiomanager.WordScrambleMethod(0);

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            correctAnswer = true;   //default value
            //loop through answerWordList
            for (int i = 0; i < answerWord.Length; i++)
            {
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                {
                    correctAnswer = false; //set it false
                    break; //and break from the loop
                }
            }
            //if correctAnswer is true
            if (correctAnswer)
            {
                audiomanager.WordScrambleMethod(1);
                Debug.Log("Correct Answer");
                gameStatus = GameStatus.Next; //set the game status
                currentQuestionIndex++; //increase currentQuestionIndex


                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.5f); //go to next question
                }
                else if (currentQuestionIndex >= questionDataScriptable.questions.Count)
                {
                    pzm.score++;
                    playermanager.progresplayer[playermanager.nourut].current++;//bar player
                    Destroy(this.gameObject);
                    solved = true;
                    Debug.Log("Game Complete"); //else game is complete
                }
            }
            else
            {
                time.waktu -= 5;
                audiomanager.popupMetohod(3);
                ResetQuestion();
            }
        }
    }

    public void ResetLastWord()
    {
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);
            answerWordList[currentAnswerIndex].gameObject.SetActive(true);
            answerWordList[currentAnswerIndex].SetWord(answerWord[currentAnswerIndex]);
        }
    }

}

[System.Serializable]
public class QuestionData
{
    public string answer;
}

public enum GameStatus
{
    Next,
    Playing
}
