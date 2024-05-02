using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6Lib.Class {
    public class SubscriberA : ISubscriber {
        public void update(string eventname) {
            Console.WriteLine($"Subscriber:A, Event:{eventname}");
        }
    }
    public class SubscriberB : ISubscriber {
        public void update(string eventname) {
            Console.WriteLine($"Subscriber:B, Event:{eventname}");
        }
    }
    public class SubscriberC : ISubscriber {
        public void update(string eventname) {
            Console.WriteLine($"Subscriber:C, Event:{eventname}");
        }
    }
}
