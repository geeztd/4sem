using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6Lib.Class {
    internal class Publisher {

        private string _eventname;

        private readonly List<ISubscriber> _observers = new();
        public string Eventname { get => _eventname; }
        public Publisher(string eventname) {
            _eventname = eventname;
        }

        public void subscribe(ISubscriber subscriber) {
            _observers.Add(subscriber);
        }
        public bool unsubscribe(ISubscriber subscriber) {
            return _observers.Remove(subscriber);
        }

        public int nonify() {

            int notifiedCount = 0;
            foreach (var observer in _observers) {
                observer.update(_eventname);
                notifiedCount++;
            }
            return notifiedCount;
        }
    }
}
