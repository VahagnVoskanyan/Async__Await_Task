namespace Async__Await_Task
{
    public static class MasterThreadAndTasks
    {
        public static void Run()
        {
            Console.WriteLine("Start");
            Console.WriteLine("Thread check 1: " + Environment.CurrentManagedThreadId);
            
            var task = new Task(() =>
            {
                Console.WriteLine("Thread check 2: " + Environment.CurrentManagedThreadId);
                Console.WriteLine("Adding Number Start");
                var result = AddNumbers(3, 4);
                Console.WriteLine("Adding Number Finish");
            });
            // Creates new thread to run the task
            task.Start();

            Console.WriteLine("Finish");

            Console.WriteLine("Thread check 3: " + Environment.CurrentManagedThreadId);

            //Task.WaitAny(task);
            task.Wait(); // Wait for the task to complete

            Console.WriteLine("Thread check 4: " + Environment.CurrentManagedThreadId);
            
            Console.ReadLine(); // Wait 
        }

        static int AddNumbers(int a, int b)
        {
            // This method adds two numbers synchronously.
            Console.WriteLine("Adding numbers on thread: " + Environment.CurrentManagedThreadId);
            return a + b;
        }
    }
}
