using BallyTech.Infrastructure.Hosting;
using System;
using System.Collections.Generic;

namespace SG.NightlyAuditServer
{
    public static class Runtime
    {
        private static readonly Dictionary<String, ServiceHostController> _hostControllers
               = new Dictionary<string, ServiceHostController>();

        public static void Add(String serviceName, IServiceHost host)
        {
            if (_hostControllers.ContainsKey(serviceName))
            {
                throw new InvalidOperationException("Service already added to Platform Service Host");
            }

            _hostControllers.Add(serviceName, new ServiceHostController(host));
        }

        public static void Remove(String serviceName, IServiceHost host)
        {
            if (!_hostControllers.ContainsKey(serviceName))
            {
                throw new InvalidOperationException("Service not to Platform Service Host");
            }

            var controller = _hostControllers[serviceName];
            if (controller.Running)
            {
                controller.StopHost();
            }
            _hostControllers.Remove(serviceName);
        }

        public static void RestartHost(String serviceName)
        {
            ShutdownHost(serviceName);
            StartHost(serviceName);
        }

        public static void ShutdownHost(String serviceName)
        {
            if (!_hostControllers.ContainsKey(serviceName))
            {
                throw new InvalidOperationException("Service added not to Platform Service Host");
            }

            var controller = _hostControllers[serviceName];
            if (!controller.Running) throw new InvalidOperationException("Host not running.");

            controller.StopHost();
        }

        public static void ShutdownHosts()
        {
            foreach (var controller in _hostControllers.Values)
            {
                if (controller.Running)
                {
                    controller.StopHost();
                }
            }
        }

        public static void StartHost(String serviceName)
        {
            if (!_hostControllers.ContainsKey(serviceName))
            {
                throw new InvalidOperationException("Service added not to Platform Service Host");
            }

            var controller = _hostControllers[serviceName];
            if (controller.Running) throw new InvalidOperationException("Host already running.");

            controller.StartHost();
        }

        public static void StartHosts()
        {
            foreach (var controller in _hostControllers.Values)
            {
                if (!controller.Running)
                {
                    controller.StartHost();
                }
            }
        }
    }
}
