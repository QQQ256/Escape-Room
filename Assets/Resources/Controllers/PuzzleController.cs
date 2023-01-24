using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �������������˳��ֻ�ܰ���1-2-3��˳��������
/// </summary>
public class PuzzleController : MonoBehaviour
{
    public class Puzzle
    {
        public int puzzleNumber;
        public bool isFinished;

        public Puzzle(int puzzleNumber, bool isFinished)
        {
            this.puzzleNumber = puzzleNumber;
            this.isFinished = isFinished;
        }
    }

    public Puzzle puzzle1;
    public Puzzle puzzle2;
    public Puzzle puzzle3;

    public static PuzzleController instance;

    private string curPwd = ""; // �洢room2 puzzle 1�е����ڵ㰴ֵ

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        puzzle1 = new Puzzle(1, true);

        // 2 �� 3 Ĭ������Ϊtrue����ʹ�õ�ʱ���������ΪFALSE�����������ΪTRUE
        puzzle2 = new Puzzle(2, true);
        puzzle3 = new Puzzle(3, true);

        InitForRoom1();
        InitForRoom2();

    }

    private void InitForRoom1() 
    {
        if (SceneManager.GetActiveScene().name == "Room1")
        {
            AudioController.PlayAudio(AudioName.Audio2);
            AudioController.PlayAudio(AudioName.BGM1);
            StartCoroutine(gameObject.GetComponentInChildren<Room1_Puzzle1>().WaitForAudio2(AudioName.Audio2));

            // for test 
            //AudioController.PlayAudio(AudioName.FeedbackAudio1);
            //StartCoroutine(this.gameObject.GetComponentInChildren<Room1_Puzzle1>().WaitForAudio2(AudioName.FeedbackAudio1));
        }
    }

    private void InitForRoom2()
    {
        if (SceneManager.GetActiveScene().name == "Room2")
        {
            // �ȹر����ٵĽ���
            this.gameObject.GetComponentInChildren<Room2_Puzzle1>().OnStartDisableRoom2Puzzle1Object();
            AudioController.PlayAudio(AudioName.Audio5);
            StartCoroutine(this.gameObject.GetComponentInChildren<Room2_Puzzle1>().WaitForAudio5(AudioName.Audio5));
        }
    }

    #region Room1 puzzle 1
    public void EnableRoom1Puzzle1()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle1>().enabled = true;
    }

    public void DisableRoom1Puzzle1()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle1>().enabled = false;
    }

    public void StartRoom1Puzzle1()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle1>().StartRoom1Puzzle1();
    }

    #endregion

    #region Room1 puzzle 2
    public void EnableRoom1Puzzle2()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle2>().enabled = true;
    }

    public void DisableRoom1Puzzle2()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle2>().enabled = false;
    }

    public void StartRoom1Puzzle2()
    {
        AudioController.PlayAudio(AudioName.Audio3);
        StartCoroutine(this.gameObject.GetComponentInChildren<Room1_Puzzle2>().WaitForAudio3(AudioName.Audio3));

        // test 
        //AudioController.PlayAudio(AudioName.FeedbackAudio2);
        //StartCoroutine(this.gameObject.GetComponentInChildren<Room1_Puzzle2>().WaitForAudio3(AudioName.FeedbackAudio2));
    }

    public void StartGamePuzzle2()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle2>().StartRoom1Puzzle2();
        gameObject.GetComponentInChildren<Room1_Puzzle2>().EnablePuzzleCamera();
    }
    #endregion

    #region Room1 puzzle 3
    public void EnableRoom1Puzzle3()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle3>().enabled = true;
    }

    public void DisableRoom1Puzzle3()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle3>().enabled = false;
    }

    public void StartRoom1Puzzle3()
    {
        AudioController.PlayAudio(AudioName.Audio4);
        StartCoroutine(this.gameObject.GetComponentInChildren<Room1_Puzzle3>().WaitForAudio4(AudioName.Audio4));
        Debug.Log("StartRoom1Puzzle3 3");
    }
    
    public void StartRoom1Puzzle3Game()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle3>().StartRoom1Puzzle3Game();
    }

    public void EnableNextScene()
    {
        gameObject.GetComponentInChildren<Room1_Puzzle3>().EnableNextScene();
    }

    #endregion

    #region Room2 puzzle 1
    /// <summary>
    /// Room 2 Puzzle 1 -- Piano Puzzle
    /// </summary>
    /// <param name="hitInfo"></param>
    public void ClickNotes(RaycastHit hitInfo, HashSet<string> noteSet, List<GameObject> noteList, string orderPwd)
    {
        string name = hitInfo.collider.name.Substring(0, 1);
        if (!noteSet.Contains(name))
        {
            noteSet.Add(name);
            noteList.Add(hitInfo.transform.gameObject);
            curPwd += name;
            hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 1, 1);

            Debug.Log(curPwd);
            Debug.Log(orderPwd);

            if (curPwd.Length == orderPwd.Length)
            {
                if (orderPwd.Equals(curPwd))
                {
                    Debug.Log("right answer!");
                    Room2Puzzle1RightAnswer();
                }
                else
                {
                    Debug.Log("wrong answer!");
                    Room2Puzzle1WrongAnswer(noteSet, noteList);
                }
            }
        }
    }

    public void Room2Puzzle1RightAnswer()
    {
        puzzle1.isFinished = true;
        puzzle2.isFinished = false;
        // �رյ�һ��puzzle����
        DisableRoom2Puzzle1();
        // �رյ���
        gameObject.GetComponentInChildren<Room2_Puzzle1>().OnStartDisableRoom2Puzzle1Object();
        // ����Ƶ������Ƶ��ͬʱ�ȴ�����6����
        VideoController.instance.PlayVideo(1);
    }

    public void Room2Puzzle1WrongAnswer(HashSet<string> noteSet, List<GameObject> noteList)
    {
        noteSet.Clear();
        foreach (var note in noteList)
        {
            note.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
        noteList.Clear();
        curPwd = "";
    }

    public void EnableRoom2Puzzle1()
    {
        gameObject.GetComponentInChildren<NoteController>().enabled = true;
    }

    public void DisableRoom2Puzzle1()
    {
        gameObject.GetComponentInChildren<Room2_Puzzle1>().enabled = false;
    }

    public void EnableRoom2Puzzle1Object()
    {
        gameObject.GetComponentInChildren<Room2_Puzzle1>().EnableRoom2Puzzle1Object();
    }
    #endregion

    #region Room2 puzzle 2
    /// <summary>
    /// ˼·����������2�ű���������ƷΪ�ɽ�����layer
    /// </summary>
    public void EnableRoom2Puzzle2()
    {
        Debug.Log("eanle puzle 2");
        this.gameObject.GetComponentInChildren<Room2_Puzzle2>().enabled = true;
    }

    public void DisableRoom2Puzzle2()
    {
        this.gameObject.GetComponentInChildren<Room2_Puzzle2>().enabled = false;
    }

    public IEnumerator WaitForFlowerAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Flower animation is done!");
    }

    public void FinishPuzzle2()
    {
        // �ȴ�������������3��
        //StartCoroutine(WaitForFlowerAnimation(3f));
        // ����puzzle3
        EnableRoom2Puzzle3();
        // ��������7
        AudioController.PlayAudio(AudioName.Audio7);
        // �ȴ�����7����
        StartCoroutine(this.gameObject.GetComponentInChildren<Room2_Puzzle2>().WaitForAudio7(AudioName.Audio7));
    }
    #endregion

    #region Room2 puzzle 3
    public void EnableRoom2Puzzle3()
    {
        this.gameObject.GetComponentInChildren<Room2_Puzzle3>().enabled = true;
    }

    public void EnableRoom2Puzzle3Game()
    {
        this.gameObject.GetComponentInChildren<Room2_Puzzle3>().EnableRoom2Puzzle3Game();
    }

    public void DisableRoom2Puzzle3Game()
    {
        this.gameObject.GetComponentInChildren<Room2_Puzzle3>().DisableRoom2Puzzle3Game();
    }

    #endregion
}
