using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderBoardController : MonoBehaviour
{

    public Text Name;
    public Text Score;
    public Text Distance;
    public Text HighScores;
    private string secretKey = "GameGameGame"; // Edit this value and make sure it's the same as the one stored on the server
    private string addScoreURL = "http://johnnortz.com/addscore.php?"; //be sure to add a ? to your url
    private string highscoreURL = "http://johnnortz.com/display.php";
    private int scr;
    private int nam;
    private string key = "";

    void Start()
    {
        GetScoreRoutine();
    }


    public void GetScoreRoutine()
    {
        StartCoroutine(GetScores());
    }

    public void SendScoreRoutine()
    {

        StartCoroutine(PostScores(Name.text, (int)Score.GetComponent<Score>()._Score, (float)Distance.GetComponent<Distance>()._Distance));
    }

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name, int score, float distance)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + score + distance + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&score2=" + distance + "&hash=" + hash;
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
            Debug.Log("There was an error posting the high score: " + hs_post.error);
        }
    }

    // Get the scores from the MySQL DB to displ0ay in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        //gameObject.guiText.text = "Loading Scores";
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;
        scr = 0;
        nam = 0;
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);

            Debug.Log("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            //gameObject.guiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
            JSONObject j = new JSONObject(hs_get.text);
            accessData(j);

            //Debug.Log(hs_get.text);
            HighScores.text = hs_get.text;
        }
        HighScores.GetComponent<HighScoreFormat>().UpdateScores();
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }


        return hashString.PadLeft(32, '0');
    }

    //access data (and print it)
    void accessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    //Debug.Log(key);
                    //HighScores.GetComponent<HighScoreFormat>().names[scr] = key;
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list)
                {
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                HighScores.GetComponent<HighScoreFormat>().names[scr] = obj.str;
                //Debug.Log(obj.str);
                break;
            case JSONObject.Type.NUMBER:
                //Debug.Log("key: " + key);
                if (key == "score") HighScores.GetComponent<HighScoreFormat>().scores[scr] = (int)obj.n;
                if (key == "score2")
                {
                    HighScores.GetComponent<HighScoreFormat>().distances[scr] = (float)obj.n;
                    scr++;
                }
                
                //Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;
 

        }
    }
}
