/* Ben Scott * @evan-erdos * bescott@andrew.cmu.edu * 2015-08-22 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

namespace Adventure.EvanescentTides {
    public abstract class Thing : MonoBehaviour, IObject {
        Semaphore semaphore;
        public string Name => name;
        public Vector3 Position => transform.position;
        protected void StartSemaphore(Func<IEnumerator> c) => semaphore.Invoke(c);
        public virtual bool Fits(string s) => new Regex("\b(object)\b").IsMatch(s);
        public override string ToString() => $"{name}";
        public virtual void OnDeactivate() => semaphore?.Clear();
        protected virtual void OnDisable() => OnDeactivate();
        protected virtual void OnEnable() => Create();
        public virtual void Create() => semaphore = new Semaphore(StartCoroutine);

        public void If(bool condition, Action then) { if (condition) then(); }
        public void If(Func<bool> cond, Action then) { if (cond()) then(); }

        public Transform GetOrAdd(string name) {
            var instance = transform.Find(name);
            if (!instance) {
                instance = new GameObject(name).transform;
                instance.parent = transform; } return instance; }

        public T GetOrAdd<T>() where T : Component => GetOrAdd<T,T>();
        public T GetOrAdd<T,U>() where T : Component where U : T {
            var component = GetComponent<T>();
            if (!component) component = gameObject.AddComponent<U>();
            return component; }

        public T Get<T>() => GetComponentOrNull<T>(GetComponent<T>());
        public T GetParent<T>() => GetComponentOrNull<T>(GetComponentInParent<T>());
        public T GetChild<T>() => GetComponentOrNull<T>(GetComponentInChildren<T>());
        T GetComponentOrNull<T>(T component) => (component==null)?default(T):component;

        public T Create<T>(GameObject original) =>
            Create<T>(original, transform.position, transform.rotation);
        public T Create<T>(GameObject original,Vector3 position) =>
            Create<T>(original,position,Quaternion.identity);
        public T Create<T>(GameObject original,Vector3 position,Quaternion rotation) =>
            GetComponentOrNull<T>(Create(original,position,rotation).GetComponent<T>());

        public GameObject Create(GameObject original) =>
            Create(original,transform.position, transform.rotation);
        public GameObject Create(GameObject original, Vector3 position) =>
            Create(original, transform.position, Quaternion.identity);
        public GameObject Create(
                        GameObject original,
                        Vector3 position,
                        Quaternion rotation) =>
                Instantiate(original,position,rotation) as GameObject;

        protected Coroutine Loop(YieldInstruction wait, Action func) {
            return StartCoroutine(Looping());
            IEnumerator Looping() { while (true) yield return Wait(wait,func); } }

        protected Coroutine Wait(YieldInstruction wait, Action func) {
            return StartCoroutine(Waiting());
            IEnumerator Waiting() { yield return wait; func(); } }
    }
}