using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website
{
    public class LoggingContext : IDisposable
    {
        string Name;
        public LoggingContext(string name, Action action)
        {
            Name = name;
            Console.WriteLine("Started " + Name);
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to run " + Name + " because " + ex.ToFullMessage());
                throw;
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Finished running " + Name);
        }
    }
}
