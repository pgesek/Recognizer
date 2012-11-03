using Recognizer.IO;

namespace Recognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Recognizer recognizer = new Recognizer();
            IInputReader inputReader = new FileReader(Properties.Settings.Default.InputFile);
            IInputReader glossaryReader = new FileReader(Properties.Settings.Default.GlossaryFile);

            recognizer.Run(inputReader, glossaryReader);
        }
    }
}
