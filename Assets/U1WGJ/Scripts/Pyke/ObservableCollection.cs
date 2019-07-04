using System;
using System.Collections.Specialized;

namespace Pyke
{
    /// <summary>
    /// リストを監視するクラス。
    /// </summary>
    /// <typeparam name="T">管理するオブジェクトタイプ</typeparam>
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        EventPublisher<T> _addEventPublisher = new EventPublisher<T>();
        EventPublisher<T> _removeEventPublisher = new EventPublisher<T>();
        EventPublisher<int, int> _changeCountEventPublisher = new EventPublisher<int, int>();

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var i in e.NewItems)
                    {
                        _addEventPublisher.Publish((T)i);
                    }

                    if (e.OldItems == null)
                    {
                        _changeCountEventPublisher.Publish(0, Count);
                    }
                    else
                    {
                        _changeCountEventPublisher.Publish(e.OldItems.Count, Count);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.NewItems != null)
                    {
                        foreach (var i in e.NewItems)
                        {
                            _removeEventPublisher.Publish((T)i);
                        }
                        _changeCountEventPublisher.Publish(e.OldItems.Count, Count);
                    }
                    else
                    {
                        if (Count == 0)
                        {
                            _changeCountEventPublisher.Publish(e.OldItems.Count, Count);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        public IDisposable SubscribeAddEvent(Action<T> action)
        {
            return _addEventPublisher.Subscribe(action);
        }

        public IDisposable SubscribeRemoveEvent(Action<T> action)
        {
            return _removeEventPublisher.Subscribe(action);
        }

        public IDisposable SubscribeCountChangeEvent(Action<int, int> action)
        {
            return _changeCountEventPublisher.Subscribe(action);
        }
    }
}

