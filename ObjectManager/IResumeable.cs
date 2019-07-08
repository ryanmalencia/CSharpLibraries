using System.IO;

namespace CSharpLibraries.ObjectManager
{
    public interface IResumeable
    {
        void Resume(Stream stream);
    }
}
