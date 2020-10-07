using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject _tableEntryPrefab;
    [SerializeField] private Transform _tableEntriesContainer;

    private List<Transform> m_EntriesTransform = new List<Transform>();
    private const int _maxEntriesToShow = 10;

    void Awake()
    {
        if (_tableEntryPrefab != null)
        {
            Highscores table = loadHighscoresTable();
            List<HighScoreEntry> sortedList = table.HighscoreEntries.OrderByDescending(entry => entry.Score).ToList();

            if (table != null)
            {
                int entriesCount = 0;

                foreach (HighScoreEntry entry in sortedList)
                {
                    if (entriesCount < _maxEntriesToShow)
                    {
                        entriesCount++;
                        createHighScoreTransformList(entry, _tableEntriesContainer, m_EntriesTransform);
                    }
                }
            }
        }
    }

    private string getRankString(int i_Rank)
    {
        string rankString;

        switch (i_Rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = string.Format("{0}TH", i_Rank);
                break;
        }

        return rankString;
    }

    private void updateEntryText(GameObject i_EntryToEdit, string i_RankString, int i_Score, string i_Date)
    {
        i_EntryToEdit.transform.Find("Pos_text").GetComponent<Text>().text = i_RankString;
        i_EntryToEdit.transform.Find("Score_text").GetComponent<Text>().text = i_Score.ToString();
        i_EntryToEdit.transform.Find("Date_text").GetComponent<Text>().text = i_Date;
    }

    private void createHighScoreTransformList(HighScoreEntry i_Entry, Transform i_Container, List<Transform> i_TransformList)
    {
        float containerZPos = i_Container.position.z;
        float containerXPos = i_Container.position.x;
        float containerYPos = i_Container.position.y;
        float entryGap;

        if (i_TransformList.Count == 0)
        {
            entryGap = 6f;
        }
        else
        {
            entryGap = 6f - 2.25f * i_TransformList.Count;
        }

        GameObject entry = Instantiate(_tableEntryPrefab, new Vector3(containerXPos - 2.5f, containerYPos + entryGap, containerZPos), Quaternion.identity);
        entry.transform.parent = i_Container;

        string rankString = getRankString(i_TransformList.Count + 1);
        updateEntryText(entry, rankString, i_Entry.Score, i_Entry.Date);

        i_TransformList.Add(entry.transform);
    }

    public void AddHighscoreEntry(int i_Score)
    {
        HighScoreEntry newEntry = new HighScoreEntry { Score = i_Score, Date = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };
        Highscores highscoresTable = loadHighscoresTable();

        highscoresTable.HighscoreEntries.Add(newEntry);

        saveHighscoresTable(highscoresTable);
    }

    private void saveHighscoresTable(Highscores highscoresTable)
    {
        string json = JsonUtility.ToJson(highscoresTable);
        PlayerPrefs.SetString("highscoresTable", json);
        PlayerPrefs.Save();
    }

    private Highscores loadHighscoresTable()
    {
        Highscores table;

        if (PlayerPrefs.HasKey("highscoresTable"))
        {
            string jsonString = PlayerPrefs.GetString("highscoresTable");
            table = JsonUtility.FromJson<Highscores>(jsonString);
        }
        else
        {
            table = new Highscores();
            table.HighscoreEntries = new List<HighScoreEntry>();
        }

        return table;
    }

    public class Highscores
    {
        public List<HighScoreEntry> HighscoreEntries;
    }

    [System.Serializable]
    public class HighScoreEntry
    {
        [SerializeField] private int _score;
        public int Score { get { return this._score; } set { this._score = value; } }
        [SerializeField] private string _date;
        public string Date { get { return this._date; } set { this._date = value; } }
    }
}

