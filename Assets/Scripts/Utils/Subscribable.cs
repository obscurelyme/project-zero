using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zero
{
  public class Observer<T> : IObserver<T>
  {
    public Observer() { }

    public virtual void Subscribe(IObservable<T> provider)
    {
      if (provider != null)
      {
        provider.Subscribe(this);
      }
    }

    public virtual void OnCompleted()
    {
      Debug.Log($"OnComplete");
    }

    public virtual void OnError(Exception e) { }

    public virtual void OnNext(T nextValue)
    {
      Debug.Log($"OnNext: {nextValue}");
    }
  }

  public class Subscribable<T> : IObservable<T>
  {
    private List<IObserver<T>> observers;
    public Subscribable()
    {
      observers = new List<IObserver<T>>();
    }
    public IDisposable Subscribe(IObserver<T> observer)
    {
      if (!observers.Contains(observer))
      {
        observers.Add(observer);
      }
      return new Unsubscriber<T>(observers, observer);
    }

    public void Fire(T data)
    {
      foreach (var observer in observers)
      {
        observer.OnNext(data);
      }
    }

    private class Unsubscriber<D> : IDisposable
    {
      private List<IObserver<D>> _observers;
      private IObserver<D> _observer;

      public Unsubscriber(List<IObserver<D>> observers, IObserver<D> observer)
      {
        this._observers = observers;
        this._observer = observer;
      }
      public void Dispose()
      {
        if (_observer != null && _observers.Contains(_observer))
        {
          _observers.Remove(_observer);
        }
      }
    }
  }
}

