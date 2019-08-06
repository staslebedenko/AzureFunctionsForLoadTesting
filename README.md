# Serverless API for scale-out testing of Azure functions 
The repository contains a C# project with a set of 5 Azure Functions and Function Proxy that
can be used for the stress testing of the App Service Consumption and Premium plans 8-).

The primary goal is to observe and evaluate the scale-out behavior of a serverless application in Azure.

Functions decorated with a singleton attribute can is for experiments with scalability limits.
You can find more about that topic in my article (friend link at medium are rather long).

https://medium.com/@staslebedenko/azure-functions-limiting-throughput-and-scalability-of-a-serverless-app-5b1c381491e3?source=friends_link&sk=d81cc0064cf2db7c82c43b410e0aa899


The level endpoint parameter can is set in the range from 0 to 100.
You can run this solution locally or deploy it to your Azure function.

Http Functions:
        
        Load: [GET] http://localhost:7071/api/load/{level}
        LoadCpu: [GET] http://localhost:7071/api/cpu/{level}
        LoadMemory: [GET] http://localhost:7071/api/memory/{level}
        SingletonCpu: [GET] http://localhost:7071/api/singlecpu/{level}
        SingletonMemory: [GET] http://localhost:7071/api/singlememory/{level}
