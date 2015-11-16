using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace WebApiRedisProvider.Extensions
{
    public static class Watcher
    {
        private static Stopwatch _stopWatch = new Stopwatch();

        public static TimeSpan Watch(this Action action)
        {
            _stopWatch.Restart();
            action();
            _stopWatch.Stop();
            return _stopWatch.Elapsed;
        }
    }
}
