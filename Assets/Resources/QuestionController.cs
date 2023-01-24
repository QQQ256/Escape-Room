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
    [Header("十个问题的UI")]
    public Image[] questionImages;

    [Header("结束问题UI")]
    public Image[] endingImages;

    [Header("每轮的时间分配")]
    public float[] roundTime;

    [Header("每轮多久闪给一次答案")]
    public float[] shineTime;

    [Header("每轮海报暂停时间")]
    public float[] posterWaitTime;

    [Header("所有问题的答案图片的list")]
    public AnswerImage listOfAnswers;

    [Header("现在是第几轮")]
    public int round;

    private int[] rawRoundAnswer;
    private int[] clickTime; // 每轮要按几次

    private string currentAnswer; // 存储每轮的结果值，进入下一轮时清空
    private float fTime; // 闪灯计时器
    private float rTime; // 每轮计时器

    [SerializeField]
    private int currentIndex; // 当前答题坐标
    private int innerListIndex; // 存list内的list的下标

    private Dictionary<int, string> innerDictionary;

    private bool isUnderQuestion;
    private bool isEnd; // 结束bool
    private bool shouldShine;
    [SerializeField]
    private float endTime;
    [SerializeField]
    private int endImageIndex;

    // Start is called before the first frame update
    void Start()
    {
        //// 从1开始，index0的位置不用
        currentAnswer = "";
        currentIndex = 0;
        innerListIndex = 0;
        endImageIndex = 0;
        endTime = 5f;
        // 3代表C，134代表ACD
        rawRoundAnswer = new int[] { 0, 3, 134, 14, 1, 1234, 122, 4444, 33, 33, 111111 };
        // 点击的次数，超过这个次数说明答案超了
        clickTime = new int[] { 0, 1, 3, 2, 1, 4, 3, 5, 2, 2, 6 };
        // for test
        // 如果想快速测试，请注释掉Puzzle controller里的两段音频2&3，注释掉上面这两个，打开下面这两个
        // 打开答题后快速点击A即可
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
    /// 玩家点击答案时将答案加入结果集
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
            // 答错音效
            AudioController.PlayAudio(AudioName.FeedbackAudio1);
            NextPageForQuestions();
        }
        else if(!CheckAnswer(currentIndex) && currentAnswer.Length == clickTime[currentIndex + 1])
        {
            // 答错音效
            AudioController.PlayAudio(AudioName.FeedbackAudio2);
        }
    }


    /// <summary>
    /// button event
    /// 结束答题
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
    /// 显示答案
    /// </summary>
    private void EnableAnswers()
    {
        // 122 对于122来说，一闪一次，二闪两次
        int shineTime = rawRoundAnswer[currentIndex + 1].ToString().Length;

        // TODO 每一轮按照设定评率闪烁
        if (innerListIndex >= shineTime)
        {
            DisableAllAnswers();
            return;
        }

        // 这里是因为在关闭所有的答案之后，如果瞬间打开相同的那个答案
        // 视觉效果看起来是没有提示的，瞬间关闭打开后相当于静止状态
        // 所以这里延时0.1f
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
    /// 根据计算出的
    /// </summary>
    private void EnableAnswersBasedOnTimeFrequency(int[] array)
    {
        // 根据数组的名称，获取对应的下标
        // 比如answer_C，对应的list的下标是1
        // answer_A,b,c,d并不都存在于list中，所以需要通过获取来执行enable/disable的操作
        List<string> tempList = new List<string>();
        foreach (Image t in listOfAnswers.list[currentIndex + 1].list)
        {
            // A - 0
            // C - 1
            tempList.Add(t.gameObject.name);
        }
        // 如果index = 2 的这个值是2，说明B需要亮两次
        for (int i = 1; i < array.Length; i++)
        {
            // 假设是C，那么应该闪listOfAnswers.list[currentIndex + 1].list[1]这个值
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
    /// 取消所有答案显示
    /// </summary>
    private void DisableAllAnswers()
    {
        foreach (Image t in listOfAnswers.list[currentIndex + 1].list)
        {
            t.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 进入下一页面
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

        // 重置每轮的计时器rTime
        shouldShine = false;
        rTime = 0f;
    }

    /// <summary>
    /// 判断玩家答案是否正确
    /// 这里得改改，这里按顺序来的比如acd = acd才对，但acd - adc就不对
    /// </summary>
    private bool CheckAnswer(int answer)
    {
        return rawRoundAnswer[answer + 1] == int.Parse(currentAnswer);
    }

    /// <summary>
    /// 将赋值的answer转换成对应的答案
    /// 例如输入122
    /// 得到的就是1两次，2一次
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
    /// 进入下一轮或答案出错，则清空当前答案
    /// </summary>
    private void ClearCurrentAnswer()
    {
        currentAnswer = "";
    }

    /// <summary>
    /// 隐藏当前page
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
    /// 结束按钮专用
    /// </summary>
    private void NextPageForEnd()
    {
        // 最终结束
        if (endImageIndex >= endingImages.Length)
        {
            isEnd = false;
            GameController.instance.EnableCameraMovement();
            this.gameObject.GetComponent<QuestionController>().enabled = false;

            for (int i = 0; i < endingImages.Length; i++)
            {
                endingImages[i].gameObject.SetActive(false);
            }

            // 开始华容道机关
            PuzzleController.instance.StartRoom1Puzzle2();
            GameController.instance.EnableSwitchButton();
            return;
        }
        endingImages[endImageIndex].gameObject.SetActive(true);
        endImageIndex++;
    }

    // 开启下一轮
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
        // T12 & 13 不会消失
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
