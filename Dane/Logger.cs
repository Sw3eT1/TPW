using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class Logger
    {
        private static Logger instance;
        private static readonly object instanceLock = new object();
        private ConcurrentQueue<string> logQueue;
        private CancellationTokenSource cancellationTokenSource;
        private Task loggingTask;
        private string logFilePath;

        private Logger()
        {
            logQueue = new ConcurrentQueue<string>();
        }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new Logger();
                        }
                    }
                }
                return instance;
            }
        }

        public void StartLogging()
        {
            if (loggingTask != null && !loggingTask.IsCompleted)
            {
                StopLogging();
            }

            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"simulation_log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            loggingTask = Task.Run(async () =>
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
                {
                    try
                    {
                        await writer.WriteLineAsync($"[{DateTime.Now:HH:mm:ss.fff}] --- Log Started ---");

                        while (!token.IsCancellationRequested || !logQueue.IsEmpty)
                        {
                            if (logQueue.TryDequeue(out string logEntry))
                            {
                                await writer.WriteLineAsync($"[{DateTime.Now:HH:mm:ss.fff}] {logEntry}");
                            }
                            else
                            {
                                await Task.Delay(100, token);
                            }
                        }
                        await writer.WriteLineAsync($"[{DateTime.Now:HH:mm:ss.fff}] --- Log Stopped ---");
                    }
                    catch (TaskCanceledException)
                    {
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during logging: {ex.Message}");
                    }
                }
            }, token);
        }

        public void StopLogging()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                try
                {
                    loggingTask?.Wait();
                }
                catch (AggregateException ex)
                {
                    ex.Handle(e => e is TaskCanceledException);
                }
                finally
                {
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                    loggingTask = null;
                }
            }
        }

        public void Log(string message)
        {
            logQueue.Enqueue(message);
        }
    }
}
