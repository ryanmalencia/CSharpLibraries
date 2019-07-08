using System.Collections.Generic;

namespace CSharpLibraries.RXCS
{
    public sealed class EventService
    {
        private static uint m_currentId = 1;
        private Dictionary<uint, EventSource> m_sourceMap = new Dictionary<uint, EventSource>();
        private Dictionary<uint, EventSink> m_sinkMap = new Dictionary<uint, EventSink>();
        private static object registerSinkLock = new object();
        private static object registerSourceLock = new object();

        private EventService()
        {

        }
        
        static EventService()
        {

        }

        public static EventService Instance { get; } = new EventService();

        public void OnObjectChanged(object sender, ObjectChangedEventArgs e)
        {

        }

        public void RegisterSink(EventSink sink)
        {
            if (!m_sinkMap.ContainsValue(sink))
            {
                lock (registerSinkLock)
                {
                    sink.Id = m_currentId;
                    m_sinkMap.Add(sink.Id, sink);
                    ++m_currentId;
                }
            }
        }

        public void UnregisterSink(uint id)
        {
            if (m_sinkMap.TryGetValue(id, out EventSink source))
            {
                m_sinkMap.Remove(id);
            }
        }

        public void RegisterSource(EventSource source)
        {
            if (!m_sourceMap.ContainsValue(source))
            {
                lock (registerSourceLock)
                {
                    source.Id = m_currentId;
                    source.OnObjectChanged += OnObjectChanged;
                    m_sourceMap.Add(source.Id, source);
                    ++m_currentId;
                }
            }
        }

        public void UnregisterSource(uint id)
        {
            if (m_sourceMap.TryGetValue(id, out EventSource source))
            {
                m_sourceMap.Remove(id);
            }
        }

        public bool Unsubscribe(uint sinkId, uint sourceId)
        {
            if (m_sourceMap.TryGetValue(sourceId, out EventSource source) && m_sinkMap.TryGetValue(sourceId, out EventSink sink))
            {
                source.OnObjectChanged -= sink.OnObjectChanged;
                return true;
            }
            return false;
        }

        public bool Subscribe(uint sinkId, uint sourceId)
        {
            if (m_sourceMap.TryGetValue(sourceId, out EventSource source) && m_sinkMap.TryGetValue(sourceId, out EventSink sink))
            {
                source.OnObjectChanged += sink.OnObjectChanged;
                return true;
            }
            return false;
        }

        public bool Unsubscribe(EventSink sink, uint sourceId)
        {
            if (m_sourceMap.TryGetValue(sourceId, out EventSource source))
            {
                source.OnObjectChanged -= sink.OnObjectChanged;
                return true;
            }
            return false;
        }

        public bool Subscribe(EventSink sink, uint sourceId)
        {
            if(m_sourceMap.TryGetValue(sourceId, out EventSource source))
            {
                source.OnObjectChanged += sink.OnObjectChanged;
                return true;
            }
            return false;
        }

        public bool Subscribe(EventSink sink, EventSource source)
        {
            source.OnObjectChanged += sink.OnObjectChanged;
            return true;
        }

        public bool Unsubscribe(EventSink sink, EventSource source)
        {
            source.OnObjectChanged -= sink.OnObjectChanged;
            return true;
        }
    }
}
