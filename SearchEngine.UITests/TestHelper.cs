using System.Threading;

namespace SearchEngine.UITests
{
    internal static class TestHelper
    {
        //Denne klasse skal bruges til at sænke hastigheden for vores test-browser interaktioner. 

        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }

    }
}
