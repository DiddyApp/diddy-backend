# diddy-backend

This is the backend for the Diddy mobile app. It contains the infrastructure as code, using the AWS .NET CDK, as well as the code for all the lambda functions.

## Structure

The `./src` folder contains a VS solution called  `Diddy.sln`. The solution holds the projects for both the infrastructure and the lambda functions themselves.

Inside the `./src/infrastructure` folder, you can find the CDK project that deploys all the resources - lambda functions, database tables, APIs, etc.
Inside the `./src/lambdas` folder, you can find multiple C# projects for each set of lambdas (e.g. Authentication or Goals). 
