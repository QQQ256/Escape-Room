using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AnswerList
{
    public List<Image> list;
}
/// <summary>
/// Index ��1��ʼ
/// </summary>
[Serializable]
public class AnswerImage
{
    public List<AnswerList> list;
}
