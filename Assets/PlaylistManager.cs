using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaylistManager : MonoBehaviour
{


    public int currentSong = -1;
    public float timer = 0.1f;
    public float threatenedVolume = 0.4f;
    public float normalVolume = 1;


    private PlayerStats playerStats;
    public AudioSource[] songList;
    private System.Random rnd = new System.Random();
    private bool shuffledPlaylist = false;

    private Settings settings;

    public TextMeshProUGUI songName;
    public TextMeshProUGUI artistName;
    public Image emojiImage;
    public Sprite deadInside;

    public Animator newSongAni;

    public AudioSource murderSong;
    public bool murderstuff = false;

    void Awake()
    {
        songList = GetComponents<AudioSource>();
        settings = GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>();
        playerStats = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>();
    }

    // Use this for initialization
    void Start()
    {
        //currentSong = RandomInt(-1, songList.Length - 2);

        murderSong.volume = 0;
        songList = songList.OrderBy(x => rnd.Next()).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Event System").GetComponent<KillCount>().peopleLeft > 0)
        {
            if (timer <= 0)
            {
                if (currentSong + 1 == songList.Length) currentSong = 0;
                else currentSong++;

                timer = songList[currentSong].clip.length;
                songList[currentSong].Play();

                songName.text = songList[currentSong].clip.name.Split(';')[0];
                artistName.text = songList[currentSong].clip.name.Split(';')[1];

                newSongAni.Play("new song");
            }

            if (currentSong == songList.Length - 1)
            {
                if (!shuffledPlaylist)
                {
                    songList = songList.OrderBy(x => rnd.Next()).ToArray();
                    shuffledPlaylist = true;
                }
            }
            else
            {
                shuffledPlaylist = false;
            }

            timer -= 1 * Time.deltaTime;

            if (playerStats.threatNumber > 0)
            {
                foreach (AudioSource audio in songList)
                {
                    if (audio.volume > threatenedVolume)
                    {
                        audio.volume -= 1f * Time.deltaTime;
                    }
                    else
                    {
                        audio.volume = threatenedVolume;
                    }
                    if (!settings.settingsOpen && !settings.musicMuted)
                        audio.mute = false;
                }
            }
            else if (playerStats.deathStarted)
            {
                foreach (AudioSource audio in songList)
                {
                    if (audio.volume > 0)
                    {
                        audio.volume -= 2f * Time.deltaTime;
                    }
                    else
                    {
                        audio.mute = true;
                    }
                }
            }
            else
            {
                foreach (AudioSource audio in songList)
                {
                    if (audio.volume < normalVolume)
                    {
                        audio.volume += 0.5f * Time.deltaTime;
                    }
                    else
                    {
                        audio.volume = normalVolume;
                    }
                    if (!settings.settingsOpen && !settings.musicMuted)
                        audio.mute = false;
                }
            }
        }
        else
        {
            foreach (AudioSource audio in songList)
            {
                audio.volume -= 0.5f * Time.deltaTime;

                if (!settings.settingsOpen && !settings.musicMuted)
                    audio.mute = false;

            }

            settings.UnlockTrophy(0);

            if (playerStats.deathStarted)
            {

                if (murderSong.volume > 0)
                {
                    murderSong.volume -= 2f * Time.deltaTime;
                }
                else
                {
                    murderSong.mute = true;
                }

            }
            else
            {
                if (murderSong.volume < normalVolume)
                {
                    murderSong.volume += 0.5f * Time.deltaTime;
                }
                else
                {
                    murderSong.volume = normalVolume;
                }
                if (!settings.settingsOpen && !settings.musicMuted)
                    murderSong.mute = false;
            }

            if (!settings.settingsOpen && !settings.musicMuted)
                murderSong.mute = false;

            if (settings.musicMuted)
                murderSong.mute = true;

            murderSong.volume += 0.5f * Time.deltaTime;

            if (!murderstuff)
            {
                murderSong.Play();


                songName.text = murderSong.clip.name.Split(';')[0];
                artistName.text = murderSong.clip.name.Split(';')[1];
                emojiImage.sprite = deadInside;

                newSongAni.Play("new song");
                murderstuff = true;
            }
        }

    }


}
