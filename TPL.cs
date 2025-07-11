namespace Async_Await_Task
{
    public static class TPL
    {
        public static void Run()
        {
            RunParallelForEachN();

            CatchingEx();

            //Console.ReadLine();
        }

        static void RunParallelForEachN()
        {
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"Task {i} started on thread {Environment.CurrentManagedThreadId}");
                Task.Delay(1000); // Simulate some work
            });
        }

        static void CatchingEx()
        {
            try
            {
                Parallel.For(0, 10, i =>
                {
                    if (i == 5) throw new InvalidOperationException("An error occurred in task " + i);
                    if (i == 6) throw new InvalidOperationException("An error occurred in task " + i);

                    Console.WriteLine($"Task {i} completed successfully on thread {Environment.CurrentManagedThreadId}");
                });
            }
            // Catches all exceptions thrown in the tasks
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
