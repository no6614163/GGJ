using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : ISerializationCallbackReceiver
{
    [SerializeField] string _name;
    [SerializeField] AudioClip _clip;
    
    [SerializeField] [Range(0f, 1f)] float _volume = 0.5f;

    float realvolume = 0.5f;
    public string Name { get { return _name; } }
    public AudioClip Clip { get { return _clip; } }
    public float Volume { get { return realvolume; } }

    public void OnAfterDeserialize()
    {
        realvolume = _volume;
    }
    public void OnBeforeSerialize()
    {
        _volume = realvolume;
    }
}
[CreateAssetMenu(menuName = "Sound List")]
public class SoundList : ScriptableObject
{
    [SerializeField] string _listTitle;
    [SerializeField] List<Sound> _sounds;
    public string ListTitle { get { return _listTitle; } }
    public Sound GetSound(string name)
    {
        Sound sound = _sounds.Find(s => s.Name == name);
        return sound;
    }
    public List<Sound> GetAllSounds(string name)
    {
        List<Sound> sounds = _sounds.FindAll(s => s.Name == name);
        return sounds;
    }
}
