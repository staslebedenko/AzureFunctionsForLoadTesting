# Serverless API for scale-out testing of Azure functions 
The repository contains a C# project with a set of 5 Azure Functions and Function Proxy that
can be used for the stress testing of the App Service Consumption and Premium plans 8-).

The primary goal is to observe and evaluate the scale-out behavior of a serverless application in Azure.

Functions decorated with a singleton attribute can is for experiments with scalability limits.
You can find more about that topic in my article (friend link at medium are rather long).

https://medium.com/@staslebedenko/azure-functions-limiting-throughput-and-scalability-of-a-serverless-app-5b1c381491e3?source=friends_link&sk=d81cc0064cf2db7c82c43b410e0aa899


The level endpoint parameter can is set in the range from 0 to 100.
You can run this solution locally or deploy it to your Azure function.

Azure CLI for function app below.

        location=northeurope
        functionsGroupName=atom-serverless-demo-2019

        az group create --name $functionsGroupName --location $location

        functionsStorAccName=atomservdemo2019
        storageAccountSku=Standard_LRS

        az storage account create --name $functionsStorAccName --location $location \
        --resource-group $functionsGroupName --sku $storageAccountSku

        location=northeurope
        insightsGroupName=atom-serverless-demo-2019
        insightsName=atom-telemetry

        az resource create --resource-group $insightsGroupName --name $insightsName --resource-type "Microsoft.Insights/components" --location $location --properties '{"Application_Type":"web"}'

        insightsKey=$(az resource show -g $insightsGroupName -n $insightsName --resource-type "Microsoft.Insights/components" --query properties.InstrumentationKey) 
        echo "Insights key = " $insightsKey


        functionsGroupName=atom-serverless-demo-2019
        functionsStorAccName=atomservdemo2019
        functionAppName=AtomServDemo2019
        runtime=dotnet
        location=northeurope

        az functionapp create --resource-group $functionsGroupName \
        --name $functionAppName --storage-account $functionsStorAccName --runtime $runtime \
        --app-insights-key $insightsKey --consumption-plan-location $location

Http Functions:
        
        Load: [GET] http://localhost:7071/api/load/{level}
        LoadCpu: [GET] http://localhost:7071/api/cpu/{level}
        LoadMemory: [GET] http://localhost:7071/api/memory/{level}
        SingletonCpu: [GET] http://localhost:7071/api/singlecpu/{level}
        SingletonMemory: [GET] http://localhost:7071/api/singlememory/{level}
