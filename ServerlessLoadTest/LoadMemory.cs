using System;
using System.Collections;
using System.Threading.Tasks;

namespace ServerlessLoadTest
{
    public class LoadMemory
    {
        public async Task ExecuteCode(int level)
        {
            var loadLevel = 40000 * level;
            var testCollection = new Hashtable();
            for (var x2 = 0; x2 < loadLevel; x2++)
            {
                var entity = new TestEntity(x2, x2, x2);
                testCollection.Add(x2, entity);
                var x = Math.Exp(6);
            }

            await Task.Delay(1);
        }
    }
}
