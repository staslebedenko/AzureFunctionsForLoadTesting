using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ServerlessLoadTest
{
    public class LoadTestController
    {
        [FunctionName(nameof(LoadCpu))]
        public async Task<IActionResult> LoadCpu(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cpu/{level}")]
            HttpRequest req,
            int level,
            ILogger log)
        {
            level = GetLoadLevel(level);
            var loadCpu = new LoadCpu();
            await loadCpu.ExecuteCode(level);
            log.LogInformation($"Cpu load function completed");
            return new OkObjectResult($"Cpu load function completed");
        }


        [FunctionName(nameof(LoadMemory))]
        public async Task<IActionResult> LoadMemory(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "memory/{level}")]
            HttpRequest req,
            int level,
            ILogger log)
        {
            level = GetLoadLevel(level);
            var loadMemory = new LoadMemory();
            await loadMemory.ExecuteCode(level);
            log.LogInformation($"Memory load function completed");
            return new OkObjectResult($"Memory load function completed");
        }

        [FunctionName(nameof(Load))]
        public async Task<IActionResult> Load(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "load/{level}")]
            HttpRequest req,
            int level,
            ILogger log)
        {
            level = GetLoadLevel(level);
            var loadCpu = new LoadCpu();
            var memoryLoad = new LoadMemory();
            await loadCpu.ExecuteCode(level);
            await memoryLoad.ExecuteCode(level);
            log.LogInformation($"Load CPU and Memory function completed");
            return new OkObjectResult($"Load CPU and Memory function completed");
        }

        [Singleton(Mode = SingletonMode.Function)]
        [Singleton(Mode = SingletonMode.Listener)]
        [FunctionName(nameof(SingletonCpu))]
        public async Task<IActionResult> SingletonCpu(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "singlecpu/{level}")]
            HttpRequest req,
            int level,
            ILogger log)
        {
            level = GetLoadLevel(level);
            var loadCpu = new LoadCpu();
            await loadCpu.ExecuteCode(level);
            log.LogInformation($"Load CPU and Memory function completed");
            return new OkObjectResult($"Cpu load function completed");
        }

        [Singleton(Mode = SingletonMode.Function)]
        [Singleton(Mode = SingletonMode.Listener)]
        [FunctionName(nameof(SingletonMemory))]
        public async Task<IActionResult> SingletonMemory(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "singlememory/{level}")]
            HttpRequest req,
            int level,
            ILogger log)
        {
            level = GetLoadLevel(level);
            var loadMemory = new LoadMemory();
            await loadMemory.ExecuteCode(level);
            return new OkObjectResult($"Cpu load function completed");
        }

        private static int GetLoadLevel(int level)
        {
            return (level > 100 || level == 0) ? 100 : level;
        }
    }
}
