using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Note 在这是音符的意思
/// </summary>
public class NoteController : MonoBehaviour
{
    public GameObject[] notes;
    public string orderPassword;
    public Material currentNoteMaterial;
    public Material replacedNoteMaterial;

    [SerializeField]
    public List<GameObject> notesList = new List<GameObject>();
    private int index = -1; // 遍历list用
    private float fTime; // 计时器

    private void Awake()
    {
        // player要获取密码值，这个脚本要先算出值
        SetOrders();
    }

    void Start()
    {
    }

    void Update()
    {
        if (!PuzzleController.instance.puzzle1.isFinished)
        {
            fTime += Time.deltaTime; 
            if (fTime > 0.5f)
            {
                fTime = 0;
                ShowNotesByOrder(++index);
            }
            if (index == notesList.Count)
            {
                index = -1;
                ResetNotes();
            }
            
        }
        else
        {
            // 若已经完成该解密，则reset整个闪光音符，并disable这个脚本
            ResetNotes();
            this.GetComponent<NoteController>().enabled = false;
        }
    }

    #region public method

    #endregion

    #region private method

    // 设置点亮顺序
    private void SetOrders()
    {
        notesList = new List<GameObject>();

        for (int i = 0; i < notes.Length; i++)
        {
            orderPassword += notes[i].transform.name.ToString();
            notesList.Add(notes[i]);
        }
    }

    /// <summary>
    /// 先设置顺序，后一个个的显现
    /// </summary>
    private void ShowNotesByOrder(int index)
    {
        if (index >= notesList.Count) index = notesList.Count - 1;

        notesList[index].gameObject.GetComponent<MeshRenderer>().material = replacedNoteMaterial;
        //notesList[index].gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void ResetNotes()
    {
        foreach (var note in notesList)
        {
            note.gameObject.GetComponent<MeshRenderer>().material = currentNoteMaterial;
            //note.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    #endregion




    #region public method

    #endregion

    #region private method

    #endregion
}
