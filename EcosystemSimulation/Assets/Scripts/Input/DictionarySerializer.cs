using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Input
{
    public class DictionarySerializer : MonoBehaviour, IEnumerable<KeyValuePair<string, float>>
    {
        [SerializeField] private List<string> keys;
        [SerializeField] private List<float> values;

        public float this[string key]
        {
            get => values[keys.IndexOf(key)];
            set => values[keys.IndexOf(key)] = value;
        }

        public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
        {
            return new DictionarySerializerEnumerator(keys, values);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

class DictionarySerializerEnumerator : IEnumerator<KeyValuePair<string, float>>
{
    private List<string> _keys;
    private List<float> _values;
    private int _index = -1;

    public DictionarySerializerEnumerator(List<string> keys, List<float> values)
    {
        _keys = keys;
        _values = values;
    }

    public bool MoveNext()
    {
        _index++;
        return _index < _keys.Count;
    }

    public void Reset()
    {
        _index = -1;
    }

    object IEnumerator.Current => Current;

    public KeyValuePair<string, float> Current => new KeyValuePair<string, float>(_keys[_index], _values[_index]);
    public void Dispose()
    {
    }
}