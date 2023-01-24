using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Note ��������������˼
/// </summary>
public class NoteController : MonoBehaviour
{
    public GameObject[] notes;
    public string orderPassword;
    public Material currentNoteMaterial;
    public Material replacedNoteMaterial;

    [SerializeField]
    public List<GameObject> notesList = new List<GameObject>();
    private int index = -1; // ����list��
    private float fTime; // ��ʱ��

    private void Awake()
    {
        // playerҪ��ȡ����ֵ������ű�Ҫ�����ֵ
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
            // ���Ѿ���ɸý��ܣ���reset����������������disable����ű�
            ResetNotes();
            this.GetComponent<NoteController>().enabled = false;
        }
    }

    #region public method

    #endregion

    #region private method

    // ���õ���˳��
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
    /// ������˳�򣬺�һ����������
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
