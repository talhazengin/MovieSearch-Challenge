# MovieSearch-Challenge
MovieSearch Challenge Project.

First of all, i have to say this.\
I didn't have much time. I entertained my guests at the weekend.

# *** How to run project? ***
dotnet run -> in MovieSearch directory.\
npm start -> in moviesearch_client directory.

# *** These jobs are done. ***
Service	Oriented Architecture -> Ok\
Asp.Net	Web	API -> Ok\
Microsoft Sql (with Entity Framework) -> Ok\
12 Minute Cache -> Ok\
Dependency	Injection -> Ok\
Log -> Some ok\
Authentication -> Some ok\
10 minute data update issue -> Ok

# *** Some Bonus jobs are done. ***
Using .Net Core Framework -> Ok\
Using React	or Angular.js in frontend. -> Ok

# *** What's missing? ***
-> I had a firewall issue while using redis. So used Microsoft built-in.\
-> For logs, i used a file logging with Serilog not mongodb.\
-> Loosely coupled omdb api is missing. It didn't finish because of time problem.\

-> Authentication is missing when using Movie Search,\
the project is working "like authentication" for now.\
It didn't finish because of time problem.

-> Clear all cache is missing need to find a solution\
when using built-in Microsoft cache,\
because IDistributedCache does not have a clear method.
