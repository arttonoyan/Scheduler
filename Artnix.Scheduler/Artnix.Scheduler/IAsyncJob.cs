﻿using System.Threading.Tasks;

namespace Artnix.Scheduler
{
    /// <summary>
    /// Some work to be done.
    /// If you are relying on the library to instantiate the job, make sure you implement a parameterless constructor
    /// (else you will be getting a System.MissingMethodException).
    /// </summary>
    public interface IAsyncJob
    {
        /// <summary>
        /// Executes the job async.
        /// </summary>
        Task ExecuteAsync();
    }
}
