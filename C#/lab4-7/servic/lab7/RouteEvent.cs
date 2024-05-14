using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace servic.lab7
{

    public class WindowCommands
    {
        static WindowCommands()
        {
            Exit = new RoutedCommand("Exit", typeof(lab7Window));
        }
        public static RoutedCommand Exit { get; set; }
    }

    public class DirectEvent : RoutedEventArgs
    {
        public DirectEvent(RoutedEvent routedEvent, object source)
            : base(routedEvent, source) { }
    }

    public class TunnelingEvent : RoutedEventArgs
    {
        public TunnelingEvent(RoutedEvent routedEvent, object source)
            : base(routedEvent, source) { }
    }

    public class BubblingEvent : RoutedEventArgs
    {
        public BubblingEvent(RoutedEvent routedEvent, object source)
            : base(routedEvent, source) { }
    }
}

