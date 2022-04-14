using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=iAbaqGYdnyI

public class highscoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    public int puntajeFinal;

    int i;
    

    private void Awake()
    {
        i = 0;

        puntajeFinal = PlayerPrefs.GetInt("ScoreFinal", 0);
        Debug.Log(puntajeFinal);

        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(puntajeFinal, "");

         string jsonString = PlayerPrefs.GetString("highscoreTable");
         Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


         for(int i = 0; i < highscores.highscoreEntryList.Count; i++)
         {
             for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
             {
                 if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                 {
                     highscoreEntry tmp = highscores.highscoreEntryList[i];
                     highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                     highscores.highscoreEntryList[j] = tmp;
                 }
             }
         }

         if (highscores.highscoreEntryList.Count > 10)
         {
             for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
             {
                 highscores.highscoreEntryList.RemoveAt(10);
             }
         }
        

         highscoreEntryTransformList = new List<Transform>();
         foreach (highscoreEntry highscoreEntry in highscores.highscoreEntryList)
         {
             CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
         }
    }

    private void AddHighscoreEntry(int score, string name)
    {
        highscoreEntry highscoreEntry = new highscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<highscoreEntry> highscoreEntryList;
    }

    private void CreateHighscoreEntryTransform(highscoreEntry highscoreentry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 60f;

            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);
        i++;
            int rank = i;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            };

            entryTransform.Find("PosText-Temp").GetComponent<Text>().text = rankString;

            int score = highscoreentry.score;
            entryTransform.Find("ScoreText-Temp").GetComponent<Text>().text = score.ToString();

            string name = highscoreentry.name;
            entryTransform.Find("NameText- Temp").GetComponent<Text>().text = name;

            transformList.Add(entryTransform);
    
    }

    [System.Serializable]
    private class highscoreEntry
    {
        public int score;
        public string name;
    }
}


