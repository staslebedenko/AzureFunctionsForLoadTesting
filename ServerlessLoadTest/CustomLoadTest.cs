using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServerlessLoadTest
{
    public class CustomLoadTest
    {
        [FunctionName(nameof(LoadCpu))]
        public async Task<IActionResult> LoadCpu(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cpu/{level}")] HttpRequest req,
            int level, ILogger log)
        {
            level = (level > 100 || level == 0) ? 100 : level;
            var loadCpu = new LoadCpu();
            await loadCpu.ExecuteCode(level);
            return (ActionResult) new OkObjectResult($"Cpu load function completed");
        }

        [FunctionName(nameof(LoadMemory))]
        public async Task<IActionResult> LoadMemory(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "memory/{level}")] HttpRequest req,
            int level, ILogger log)
        {
            level = (level > 100 || level == 0) ? 100 : level;
            var loadMemory = new LoadMemory();
            await loadMemory.ExecuteCode(level);
            return (ActionResult)new OkObjectResult($"Memory load function completed");
        }

        [FunctionName(nameof(Load))]
        public async Task<IActionResult> Load(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "load/{level}")] HttpRequest req,
            int level, ILogger log)
        {
            level = (level > 100 || level == 0) ? 100 : level;
            var loadCpu = new LoadCpu();
            var memoryLoad = new LoadMemory();
            await loadCpu.ExecuteCode(level);
            await memoryLoad.ExecuteCode(level);
            
            return (ActionResult)new OkObjectResult($"Load CPU and Memory function completed");
        }

        [Singleton(Mode = SingletonMode.Function)]
        [Singleton(Mode = SingletonMode.Listener)]
        [FunctionName(nameof(SingletonCpu))]
        public async Task<IActionResult> SingletonCpu(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "singlecpu/{level}")] HttpRequest req,
            int level, ILogger log)
        {
            level = (level > 100 || level == 0) ? 100 : level;
            var loadCpu = new LoadCpu();
            await loadCpu.ExecuteCode(level);
            return (ActionResult)new OkObjectResult($"Cpu load function completed");
        }

        [Singleton(Mode = SingletonMode.Function)]
        [Singleton(Mode = SingletonMode.Listener)]
        [FunctionName(nameof(SingletonMemory))]
        public async Task<IActionResult> SingletonMemory(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "singlememory/{level}")] HttpRequest req,
            int level, ILogger log)
        {
            level = (level > 100 || level == 0) ? 100 : level;
            var loadMemory = new LoadMemory();
            await loadMemory.ExecuteCode(level);
            return (ActionResult)new OkObjectResult($"Cpu load function completed");
        }
    }
}
