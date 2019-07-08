using System;
using System.IO;

namespace CSharpLibraries.FileManagement
{
    public class FileReference
    {
        private uint m_id;

        public uint Id
        {
            get
            {
                return m_id;
            }
            set
            {
                if (m_id == 0 && value != 0)
                    m_id = value;
            }
        }

        /// <summary>
        /// Path of file reference. 
        /// </summary>
        public string Location { get; }

        private FileReference()
        {
            Location = String.Empty;
        }

        /// <summary>
        /// Constructor with Location.
        /// </summary>
        /// <param name="location">Location of file reference.</param>
        public FileReference(string location)
        {
            if (!File.Exists(location))
            {
                Location = String.Empty;
                throw new FileNotFoundException("Cannot construct FileReference with invalid path");
            }

            Location = location;
        }

        /// <summary>
        /// Overridden Equals method.
        /// </summary>
        /// <param name="obj">Other FileReference object.</param>
        /// <returns>True if equal, false if not.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return (obj is FileReference fileReference)
                && (Location.Equals(fileReference.Location));
        }

        /// <summary>
        /// Overridden GetHashCode method.
        /// </summary>
        /// <returns>GetHashCode of Location string.</returns>
        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}
