using Recognizer.IO;

namespace Recognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Recognizer recognizer = new Recognizer();
            IInputReader reader = new FileReader(Properties.Settings.Default.InputFile);

            recognizer.Run(reader);
        }
    }
}
