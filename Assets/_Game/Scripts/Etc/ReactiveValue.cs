using System;
using System.Collections.Generic;
using System.Linq;
using MemoryPack;
using Reactive.Bindings;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace _Game.Scripts.Etc
{
    [Serializable]
    [MemoryPackable]
    public partial class ReactiveValue<T>
    {
#if UNITY_EDITOR
        public class ReactiveStructValueDrawer : OdinValueDrawer<ReactiveValue<T>> 
        {
            protected override void DrawPropertyLayout(GUIContent label)
            {
                ValueEntry.Property.Children.First().Draw(label);
            }
        }
#endif
        [OnValueChanged(nameof(OnValueChange))]
        [Delayed]
        [SerializeField] private T _mValue;
        public T Value
        {
            get => _mValue;
            set
            {
                _mValue = value;
                ReactiveProperty.Value = value;
            }
        }
    
        [MemoryPackIgnore] private ReactiveProperty<T> mReactiveProperty;
        [MemoryPackIgnore] public ReactiveProperty<T> ReactiveProperty => InitializeOrReSync();
    
        public ReactiveValue()
        {
            mReactiveProperty = new ReactiveProperty<T>(_mValue);
            
        }
        [MemoryPackConstructor]
        public ReactiveValue(T value)
        {
            _mValue = value;
            mReactiveProperty = new ReactiveProperty<T>(value);
            Value = value;
        }
    
        private ReactiveProperty<T> InitializeOrReSync()
        {
            if (mReactiveProperty != null && EqualityComparer<T>.Default.Equals(mReactiveProperty.Value, Value)) return mReactiveProperty;
            mReactiveProperty = new ReactiveProperty<T>(_mValue);
            return mReactiveProperty;
        }
    
        private void OnValueChange()
        {
#if UNITY_EDITOR
            mReactiveProperty.Value = _mValue;
#endif
        }
    }
}