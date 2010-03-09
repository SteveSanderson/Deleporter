using System;
using System.Collections.Generic;

namespace WhatTimeIsIt.IntegrationTest.Infrastructure
{
    /// <summary>
    /// Since you can't usually run multiple integration tests in parallel, this approach to tidying up makes no attempt to
    /// manage separate collections of tasks for concurrently running tests. If your test suite does support parallelization
    /// to some extent, you might need to enhance this class.
    /// </summary>
    public static class TidyupUtils
    {
        private static readonly List<Action> _tasks = new List<Action>();

        public static void AddTidyupTask(Action task) { _tasks.Add(task); }

        public static void PerformTidyup()
        {
            try {
                foreach (var task in _tasks)
                    task();
            }
            finally {
                _tasks.Clear();
            }
        }
    }
}