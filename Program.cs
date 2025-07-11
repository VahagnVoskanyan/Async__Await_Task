using Async__Await_Task;

/*Console.WriteLine("Hello, World!");

await MakeTeaAsync();

Console.WriteLine();

await ThreadTask();*/

Console.WriteLine();

MasterThreadAndTasks.Run();

async Task<string> MakeTeaAsync()
{
    Console.WriteLine("Thread check 1: " + Environment.CurrentManagedThreadId);

    Task<string> boilingWater = BoilWaterAsync();

    Console.WriteLine("Thread check 2: " + Environment.CurrentManagedThreadId);

    Console.WriteLine("--take the cups out");

    Console.WriteLine("--put tea in cups");

    string water = await boilingWater;

    Console.WriteLine("Thread check 3: " + Environment.CurrentManagedThreadId);

    var tea = $"--pour {water} in cups";

    Console.WriteLine(tea);

    return tea;
}

async Task<string> BoilWaterAsync()
{
    Console.WriteLine("--Start the kettle");

    var a = 0;
    for (int i = 0; i < 100_000_0000; i++)
    {
        a += i;
    }

    Console.WriteLine("--waiting for the kettle");
    // it is synchronous till here . then after await it goes to other thread

    await Task.Delay(2000); // having await exits method here

    Console.WriteLine("--kettle finished boiling");

    return "water";
}

// in IL Task is Object (to continue in other thread)
async Task ThreadTask()
{
    var client = new HttpClient();
    Console.WriteLine("Thread check 1: " + Environment.CurrentManagedThreadId);
    var task = client.GetStringAsync("http://google.com");
    Console.WriteLine("Thread check 2: " + Environment.CurrentManagedThreadId);

    var a = 0;
    for (int i = 0; i < 1_000_0000; i++) // 100_000_0000
    {
        a += i;
    }

    var response = await task;
    // If thread must wait to response it goes back to thread pool
    // and when the response comes it continues in other thread 
    // If we already have the response before loop finishes 
    // we continue on same thread
    Console.WriteLine("Thread check 3: " + Environment.CurrentManagedThreadId);
}