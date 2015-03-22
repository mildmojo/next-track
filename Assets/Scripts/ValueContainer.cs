using UnityEngine;
using System.Collections.Generic;
// using System.Linq;

public class ValueContainer : MonoBehaviour {

  [System.Serializable]
  public class KeyValue {
    public string key;
    public string value;
  }

  public List<KeyValue> Values;
  private Dictionary<string, string> _data;

  // Use this for initialization
  void Awake() {
    BuildDict();
  }

  // Update is called once per frame
  void Update () {

  }

  public string Get(string key) {
    // return Values.Find(pair => pair.key == key).value;
    BuildDict();
    return _data[key];
  }

  public void Set(string key, string newValue) {
    BuildDict();
    _data[key] = newValue;
    // var idx = Values.FindIndex(pair => pair.key == key);
    // if (idx >= 0) {
    //   var entry = Values[idx];
    //   entry.value = newValue;
    //   Values[idx] = entry;
    // } else {
    //   var entry = new KeyValue();
    //   entry.key = key;
    //   entry.value = newValue;
    //   Values.Add(entry);
    // }
  }

  private void BuildDict() {
    if (_data != null) return;

    _data = new Dictionary<string, string>();
    foreach (var kv in Values) {
      _data[kv.key] = kv.value;
    }
  }
}
