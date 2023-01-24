using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [Header("ʮ�������UI")]
    public Image[] questionImages;

    [Header("��������UI")]
    public Image[] endingImages;

    [Header("ÿ�ֵ�ʱ�����")]
    public float[] roundTime;

    [Header("ÿ�ֶ������һ�δ�")]
    public float[] shineTime;

    [Header("ÿ�ֺ�����ͣʱ��")]
    public float[] posterWaitTime;

    [Header("��������Ĵ�ͼƬ��list")]
    public AnswerImage listOfAnswers;

    [Header("�����ǵڼ���")]
    public int round;

    private int[] rawRoundAnswer;
    private int[] clickTime; // ÿ��Ҫ������

    private string currentAnswer; // �洢ÿ�ֵĽ��ֵ��������һ��ʱ���
    private float fTime; // ���Ƽ�ʱ��
    private float rTime; // ÿ�ּ�ʱ��

    [SerializeField]
    private int currentIndex; // ��ǰ��������
    private int innerListIndex; // ��list�ڵ�list���±�

    private Dictionary<int, string> innerDictionary;

    private bool isUnderQuestion;
    private bool isEnd; // ����bool
    private bool shouldShine;
    [SerializeField]
    private float endTime;
    [SerializeField]
    private int endImageIndex;

    // Start is called before the first frame update
    void Start()
    {
        //// ��1��ʼ��index0��λ�ò���
        currentAnswer = "";
        currentIndex = 0;
        innerListIndex = 0;
        endImageIndex = 0;
        endTime = 5f;
        // 3����C��134����ACD
        rawRoundAnswer = new int[] { 0, 3, 134, 14, 1, 1234, 122, 4444, 33, 33, 111111 };
        // ����Ĵ����������������˵���𰸳���
        clickTime = new int[] { 0, 1, 3, 2, 1, 4, 3, 5, 2, 2, 6 };
        // for test
        // �������ٲ��ԣ���ע�͵�Puzzle controller���������Ƶ2&3��ע�͵�������������������������
        // �򿪴������ٵ��A����
        //rawRoundAnswer = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        //clickTime = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        isEnd = false;
        isUnderQuestion = true;
        shouldShine = false;
    }

    void Update()
    {
        rTime += Time.deltaTime;
        if (!shouldShine && rTime >= roundTime[round])
        {
            shouldShine = true;
        }

        fTime += Time.deltaTime;
        if (!isEnd && fTime >= shineTime[round] && isUnderQuestion && shouldShine)
        {
            DisableAllAnswers();
            EnableAnswers();
            fTime = 0;
        }
        if (isEnd)
        {
            endTime += Time.deltaTime;
            if (endTime >= 5f)
            {
                endTime = 0f;
                NextPageForEnd();
            }
        }
    }

    /// <summary>
    /// ��ҵ����ʱ���𰸼�������
    /// </summary>
    /// <param name="number"></param>
    public void ClickButton(string number)
    {
        if (isEnd)
        {
            return;
        }

        if (currentAnswer.Length >= clickTime[currentIndex + 1])
        {
            ClearCurrentAnswer();
        }
        currentAnswer += number;

        if (CheckAnswer(currentIndex))
        {
            // �����Ч
            AudioController.PlayAudio(AudioName.FeedbackAudio1);
            NextPageForQuestions();
        }
        else if(!CheckAnswer(currentIndex) && currentAnswer.Length == clickTime[currentIndex + 1])
        {
            // �����Ч
            AudioController.PlayAudio(AudioName.FeedbackAudio2);
        }
    }


    /// <summary>
    /// button event
    /// ��������
    /// </summary>
    public void FinishQuestion()
    {
        Debug.Log("finish question button is clicked");
        isEnd = true;
        Invoke("WaitForFinishQuestion", posterWaitTime[round]);
    }

    private void WaitForFinishQuestion()
    {
        questionImages[currentIndex].gameObject.SetActive(false);
    }

    /// <summary>
    /// ��ʾ��
    /// </summary>
    private void EnableAnswers()
    {
        // 122 ����122��˵��һ��һ�Σ���������
        int shineTime = rawRoundAnswer[currentIndex + 1].ToString().Length;

        // TODO ÿһ�ְ����趨������˸
        if (innerListIndex >= shineTime)
        {
            DisableAllAnswers();
            return;
        }

        // ��������Ϊ�ڹر����еĴ�֮�����˲�����ͬ���Ǹ���
        // �Ӿ�Ч����������û����ʾ�ģ�˲��رմ򿪺��൱�ھ�ֹ״̬
        // ����������ʱ0.1f
        Invoke("Wait", 0.1f);
    }

    /// <summary>
    /// invoke function
    /// </summary>
    private void Wait()
    {
        listOfAnswers.list[currentIndex + 1].list[innerListIndex].gameObject.SetActive(true);

        innerListIndex++;
    }

    /// <summary>
    /// ���ݼ������
    /// </summary>
    private void EnableAnswersBasedOnTimeFrequency(int[] array)
    {
        // ������������ƣ���ȡ��Ӧ���±�
        // ����answer_C����Ӧ��list���±���1
        // answer_A,b,c,d������������list�У�������Ҫͨ����ȡ��ִ��enable/disable�Ĳ���
        List<string> tempList = new List<string>();
        foreach (Image t in listOfAnswers.list[currentIndex + 1].list)
        {
            // A - 0
            // C - 1
            tempList.Add(t.gameObject.name);
        }
        // ���index = 2 �����ֵ��2��˵��B��Ҫ������
        for (int i = 1; i < array.Length; i++)
        {
            // ������C����ôӦ����listOfAnswers.list[currentIndex + 1].list[1]���ֵ
            if (array[i] != 0)
            {
                string imageString = innerDictionary[i];
                int index = tempList.IndexOf(imageString);
                Debug.Log("caluclated image name" + imageString);
                Debug.Log("caluclated index" + index);
                listOfAnswers.list[currentIndex + 1].list[index].gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// ȡ�����д���ʾ
    /// </summary>
    private void DisableAllAnswers()
    {
        foreach (Image t in listOfAnswers.list[currentIndex + 1].list)
        {
            t.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ������һҳ��
    /// </summary>
    private void NextPageForQuestions()
    {
        currentIndex++;
        Debug.Log("NextPageForQuestions" + currentIndex);
        if (currentIndex == 10)
        {
            EnableEndPage();
            return;
        }
        innerListIndex = 0;
        ClearCurrentAnswer();
        DisableCurrentPage();
        EnableNextPage();

        // ����ÿ�ֵļ�ʱ��rTime
        shouldShine = false;
        rTime = 0f;
    }

    /// <summary>
    /// �ж���Ҵ��Ƿ���ȷ
    /// ����øĸģ����ﰴ˳�����ı���acd = acd�Ŷԣ���acd - adc�Ͳ���
    /// </summary>
    private bool CheckAnswer(int answer)
    {
        return rawRoundAnswer[answer + 1] == int.Parse(currentAnswer);
    }

    /// <summary>
    /// ����ֵ��answerת���ɶ�Ӧ�Ĵ�
    /// ��������122
    /// �õ��ľ���1���Σ�2һ��
    /// </summary>
    /// <param name="roundAnswer"></param>
    private int[] ConvertIntToArray(int number)
    {
        char[] ch = number.ToString().ToCharArray();
        int[] tempArray = new int[5];
        for (int i = 0; i < ch.Length; i++)
        {
            tempArray[ch[i] - '0']++;
            //Debug.Log(tempArray[i]);
        }

        return tempArray;
    }

    /// <summary>
    /// ������һ�ֻ�𰸳�������յ�ǰ��
    /// </summary>
    private void ClearCurrentAnswer()
    {
        currentAnswer = "";
    }

    /// <summary>
    /// ���ص�ǰpage
    /// </summary>
    private void DisableCurrentPage()
    {
        int prevIndex = currentIndex - 1;
        if (prevIndex >= 0)
        {
            questionImages[prevIndex].gameObject.SetActive(false);
        }
    }

    private void EnableNextPage()
    {
        questionImages[currentIndex].gameObject.SetActive(true);
    }

    private void EnableEndPage()
    {
        PuzzleController.instance.DisableRoom1Puzzle1();
        isUnderQuestion = false;
        DisableCurrentPage();
        EnableNextPage();
    }

    /// <summary>
    /// ������ťר��
    /// </summary>
    private void NextPageForEnd()
    {
        // ���ս���
        if (endImageIndex >= endingImages.Length)
        {
            isEnd = false;
            GameController.instance.EnableCameraMovement();
            this.gameObject.GetComponent<QuestionController>().enabled = false;

            for (int i = 0; i < endingImages.Length; i++)
            {
                endingImages[i].gameObject.SetActive(false);
            }

            // ��ʼ���ݵ�����
            PuzzleController.instance.StartRoom1Puzzle2();
            GameController.instance.EnableSwitchButton();
            return;
        }
        endingImages[endImageIndex].gameObject.SetActive(true);
        endImageIndex++;
    }

    // ������һ��
    /// <summary>
    /// button event
    /// </summary>
    public void StartNextRound()
    {
        round++;
        for (int i = 0; i < endingImages.Length; i++)
        {
            endingImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < questionImages.Length; i++)
        {
            questionImages[i].gameObject.SetActive(false);
        }

        if (round >= roundTime.Length)
        {
            isEnd = true;
            shouldShine = true;

            return;
        }
        questionImages[0].gameObject.SetActive(true);
        // T12 & 13 ������ʧ
        innerListIndex = 0;
        endImageIndex = 0;
        currentIndex = 0;
        currentAnswer = "";
        rTime = 0;
        fTime = 0;
        shouldShine = false;
        isUnderQuestion = true;
        isEnd = false;

    }
}
