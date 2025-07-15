namespace Async_Await_Task
{
    public class SemaphoreEx
    {
        private HttpClient _client = new() { Timeout = TimeSpan.FromSeconds(3) };
        private SemaphoreSlim _gate = new(50);

        public void Run()
        {
            Task.WaitAll(CreateCalls().ToArray());
        }

        public IEnumerable<Task> CreateCalls()
        {
            for (int i = 0; i < 500; i++)
            {
                yield return CallGoogle($"https://www.google.com");
            }
        }

        public async Task CallGoogle(string url)
        {
            try
            {
                // if no gate => it will throw exception
                // because time is out before we even can make the call
                // network card can't handle so many requests at once
                await _gate.WaitAsync();

                var response = await _client.GetAsync(url);

                // if noe
                _gate.Release();

                Console.WriteLine(response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
