using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Adventure.EvanescentTides {

    /// IObject
    /// common interface to base type of object in this system
    public interface IObject { }

    /// SpaceAction : (sender, args) => void
    /// ubiquitous event handler, with a sender and event arguments
    public delegate void WaveAction(IObject sender, WaveArgs args);

    /// DamageAction : (sender, damage) => void
    /// handler for when stuff in space get damaged
    public delegate void DamageAction(IObject sender, float damage);

    /// WaveArgs : EventArgs
    /// provides a base argument type for space events
    public class WaveArgs : EventArgs { }

    /// WaveEvent : UnityEvent
    /// a serializable event handler to expose to the editor
    [Serializable] public class WaveEvent : UnityEvent<IObject,WaveArgs> { }

    /// WaveException : error
    /// when something goes awry on the high seas
    public class WaveException : Exception {
        public WaveException(string s) : base(s) { } }
}
