using System;
using System.Threading.Tasks;

namespace ServerlessLoadTest
{
    public class LoadCpu
    {
        public async Task ExecuteCode(int level)
        {
            var loadLevel = 1000000 * (level * 10);

            for (int x2 = 0; x2 < loadLevel; x2++)
            {
                var x = Math.Exp(6);
            }

            await Task.Delay(1);
        }
    }
}
