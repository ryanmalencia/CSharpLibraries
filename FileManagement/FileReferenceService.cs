using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpLibraries.FileManagement
{
    public class FileReferenceService
    {
        private static uint m_currentId = 1;
        private Dictionary<uint, FileReference> m_refMap = new Dictionary<uint, FileReference>();
        private Dictionary<string, uint> m_locationMap = new Dictionary<string, uint>();

        private FileReferenceService()
        {

        }

        static FileReferenceService()
        {

        }

        public static FileReferenceService Instance { get; } = new FileReferenceService();

        public uint RegisterFile(string location)
        {
            try
            {
                if (m_locationMap.TryGetValue(location, out uint existingId))
                    return existingId;

                FileReference fileReference = new FileReference(location)
                {
                    Id = m_currentId
                };
                m_refMap.Add(m_currentId, fileReference);
                m_locationMap.Add(location, m_currentId);
                m_currentId++;
                return fileReference.Id;
            }
            catch(FileNotFoundException)
            {
                return 0;
            }
        }

        public FileReference GetReferenceById(uint id)
        {
            if(m_refMap.TryGetValue(id, out FileReference fileReference))
                return fileReference;
            return null;
        }

        public FileReference GetReferenceByLocation(string location)
        {
            if (m_locationMap.TryGetValue(location, out uint id))
            {
                if (m_refMap.TryGetValue(id, out FileReference fileReference))
                    return fileReference;
            }
            return null;
        }
    }
}
