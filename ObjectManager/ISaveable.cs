using System.IO;

namespace CSharpLibraries.ObjectManager
{
    public interface ISaveable
    {
        void Save(Stream stream);
    }
}
