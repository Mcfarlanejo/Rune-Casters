using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    static AudioManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public AudioSource music;
    public AudioSource enemyHit;
    public AudioSource spellCast;
    public AudioSource itemPickup;
    public AudioSource playerHit;
}
