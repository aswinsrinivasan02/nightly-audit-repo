using BallyTech.Infrastructure.Configuration;
using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.Data.Caching;
using BallyTech.Infrastructure.Data.MSSql;
using BallyTech.Infrastructure.Debugging;
using BallyTech.Infrastructure.ExceptionHandling;
using BallyTech.Infrastructure.Hosting;
using BallyTech.Infrastructure.Logging;
using BallyTech.Infrastructure.Messaging;
using BallyTech.Infrastructure.Printing;
using BallyTech.Infrastructure.Workflow;
using Configurator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using BallyTech.Infrastructure.EventBus.Configuration;
using BallyTech.Infrastructure.ExternalData.Configs;
using SG.NightlyAudit.Utilities;

namespace PlatformConfigurator
{
    public class Program
    {
        #region Private Fields

        private static IConfigService config;
        private static string configXMLPath = "ConfigXML";

        #endregion Private Fields

        #region Public Methods

        public static void Configure(IConfigService configService)
        {
            config = configService;

            Configure();
        }

        public static T Deserialize<T>(string fileName) where T : class
        {
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));

            T obj = default(T);
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                obj = (T)_xmlserializer.Deserialize(stream);
                stream.Close();
            }
            return obj;
        }

        #endregion Public Methods

        #region Private Methods

        private static void Configure()
        {
            WriteOperationSecurityConfiguration();
            WriteWorkFlowXmlConfigurations();
            WriteWorkItemTypeConfiguration();
            WriteExceptionHandlePolicy();
            LoadExceptionHandlePolicyRetryCount();
            WritetypeShardConfigurationSection();
            WriteDataConfigurationSection();
            WriteCommandBuilderConfig();
            WriteObjectBuilderConfig();
            WriteObjectProviderConfig();
            WriteQueryBuilderConfig();
            WriteTraceConfiguration();
            WriteLoggerConfiguration();
            WriteInstrumentationConfiguration();
            WriteMomConfiguration();
            WriteEventMessageConfig();
            WriteCacheConfig();
            LoadAPIService();
            ConfigureHostingEnnvironment();
            
            WriteEventBusSystemConfiguration();
            WriteEventBusMessageTypeConfiguration();
            WriteEventBusProviderConfiguration();
            WriteEventBusConsumerConfiguration();
            WriteEventBusProducerConfiguration();
            WriteNumberGeneratorConfig();
            //WriteExternalStorageProviderConfig();
        }

        private static void WriteOperationSecurityConfiguration()
        {
            Save<OperationSecurityConfiguration, OperationSecurity>("OperationSecurity",
                Path.Combine(configXMLPath, "OperationSecurityConfig.xml"),
                new Func<OperationSecurityConfiguration, IEnumerable<OperationSecurity>>(o => o.OperationSecurity),
                new Func<OperationSecurity, string>(o => o.APIName));
        }

        private static void ConfigureHostingEnnvironment()
        {
            config.Save("Hosting", "NightlyAuditRESTApi", RESTHostingType.RESTHttp);
            config.Save("WCFServer", "NightlyAuditRESTApi", "net.tcp://localhost:8001/");
            config.Save("HttpServer", "NightlyAuditRESTApi", "http://localhost:9955/");
            config.Save("SignalR", "IPAddress", "http://10.2.100.40:9089");
            config.Save("HttpServerHandler", "NightlyAuditRESTApi", "NightlyAuditAPI.Contract.HttpServerHandler, NightlyAuditAPI.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            config.Save("WCFServerHandler", "NightlyAuditRESTApi", "NightlyAuditAPI.Contract.CustomHeader, NightlyAuditAPI.Contract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            string fileName = @"ConfigXml\ServerHostConfig.xml";
            ServerHostConfig configs = Deserialize<ServerHostConfig>(fileName);
            foreach (var item in configs.Endpoints)
            {
                config.Save("WCFServer", "PlatformRESTApi." + item.ConfigName, item.HostConfig);
            }

            config.Save("Hosting", "MOM", RESTHostingType.RESTHttp);
            config.Save("WCFServer", "MOM", "net.tcp://localhost:8001/");
            config.Save("HttpServer", "MOM_NA", "http://localhost:9953/");
            string fileName1 = @"ConfigXml\ServerHostConfig.xml";
            ServerHostConfig configs1 = Deserialize<ServerHostConfig>(fileName1);
            foreach (var item in configs1.Endpoints)
            {
                config.Save("WCFServer", "MOM." + item.ConfigName, item.HostConfig);
            }
        }

        private static void CopyConfigurationdbToPlatform()
        {
            var platformConfigDbPath = Path.GetFullPath(ConfigurationManager.AppSettings.Get("PlatformConfigDbPath"));
            var PlatformAPITestConfigDbPath = Path.GetFullPath(ConfigurationManager.AppSettings.Get("PlatformAPITestConfigDbPath"));
            var configDBPath = Path.Combine(Environment.CurrentDirectory, @"ConfigStore\PlatformConfigDB.sdf");

            var resourceDbPathSource = Path.Combine(Environment.CurrentDirectory, @"ConfigStore\Resource.sdf");
            var resourceDbPathDestination = Path.GetFullPath(ConfigurationManager.AppSettings.Get("ResourceConfigDbPath"));

            File.Copy(resourceDbPathSource, resourceDbPathDestination, true);
            File.Copy(configDBPath, platformConfigDbPath, true);
          //  File.Copy(platformConfigDbPath, PlatformAPITestConfigDbPath, true);
        }

        private static void DeleleDataFromConfigTable()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("StorageProviderConnection");
            string commandText = "Delete tConfiguration";

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                using (SqlCeTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        SqlCeCommand command = new SqlCeCommand(commandText, connection, transaction);
                        command.CommandType = CommandType.Text;
                        command.Prepare();
                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (SqlCeException exception)
                    {
                        transaction.Rollback();
                        throw exception;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private static void LoadAPIService()
        {
            var webAPIServiceType = ConfigurationManager.AppSettings["WebAPIServiceType"].ToString();
            var webAPIServiceHostURI = ConfigurationManager.AppSettings["WebAPIServiceHostURI"].ToString();

            var wcfSelfHostConfigUM = new WCFSelfHostConfig(webAPIServiceType, webAPIServiceHostURI.Split(','));

            config.Save<WCFSelfHostConfig>("Platform", "PlatformAPI.RESTService", wcfSelfHostConfigUM);

            var HTTPServiceType = ConfigurationManager.AppSettings["HTTPServiceType"].ToString();
            var HTTPServiceHostURI = ConfigurationManager.AppSettings["HTTPServiceHostURI"].ToString();

            var httpSelfHostConfigUM = new WCFSelfHostConfig(HTTPServiceType, HTTPServiceHostURI.Split(','));

            config.Save<WCFSelfHostConfig>("Enterprise", "Enterprise.HTTPService", httpSelfHostConfigUM);
        }

        private static void LoadExceptionHandlePolicyRetryCount()
        {
            config.Save<int>("ExceptionPolicy", "RetryCount", 1);
        }


        private static void Main(string[] args)
        {
            Console.WriteLine("... PLATFORM CONFIGURATOR ...");
            Console.WriteLine();
            Console.WriteLine("Loading Configurations...");
            Console.WriteLine();
            DeleleDataFromConfigTable();
            Configure(new ConfigService(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath));
            CopyConfigurationdbToPlatform();
            Console.WriteLine("Configuration database loaded successfully.");
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to exit.");
            Console.ReadLine();
        }

        private static void Save<T, K>(string typeKey, string fileName, Func<T, IEnumerable<K>> getConfigs, Func<K, string> getKey) where T : class
        {
            //typeKey = null;
            T obj = Deserialize<T>(fileName);
            IEnumerable<K> configs = getConfigs(obj);

            config.Save<K>(typeKey, configs, getKey);
        }

        private static void Save<T>(string typeKey, string fileName, string key) where T : class
        {
            //typeKey = null;
            T obj = Deserialize<T>(fileName);

            config.Save<T>(typeKey, key, obj);
        }

        private static void WriteCacheConfig()
        {
            Save<CacheConfigCollection, CacheConfig>(
                          "CacheConfiguration",
                          Path.Combine(configXMLPath, "CacheConfig.xml"),
                          new Func<CacheConfigCollection, IEnumerable<CacheConfig>>(o => o.CacheCollection),
                          new Func<CacheConfig, string>(o => o.AggregateName));
        }

        private static void WriteCommandBuilderConfig()
        {
            Save<CommandBuilderConfig, CommandBuilder>(
                "CommandBuilderConfig",
                Path.Combine(configXMLPath, "CommandBuilderConfig.xml"),
                new Func<CommandBuilderConfig, IEnumerable<CommandBuilder>>(o => o.CommandBuilderCollection),
                new Func<CommandBuilder, string>(o => o.TypeName));
        }

        private static void WriteDataConfigurationSection()
        {
            Save<DataConfigurationSection, DataProviderAdapterElement>("Data.DataServices",
                 Path.Combine(configXMLPath, "DataConfigurationSection.xml"),
                 new Func<DataConfigurationSection, IEnumerable<DataProviderAdapterElement>>(o => o.DataServiceElementCollection),
                 new Func<DataProviderAdapterElement, string>(o => o.Name));
        }

        private static void WriteEventMessageConfig()
        {
            Save<EventMessageConfigCollection, EventMessageConfig>(
                          "EventMessageConfig",
                          Path.Combine(configXMLPath, "EventMessageConfig.xml"),
                          new Func<EventMessageConfigCollection, IEnumerable<EventMessageConfig>>(o => o.EventMessageCollection),
                          new Func<EventMessageConfig, string>(o => o.EventName));
        }

        private static void WriteExceptionHandlePolicy()
        {
            Save<ExceptionHandlePolicies, ExceptionHandlePolicy>("ExceptionHandlePolicy",
                Path.Combine(configXMLPath, "ExceptionHandlePolicy.xml"),
                new Func<ExceptionHandlePolicies, IEnumerable<ExceptionHandlePolicy>>(o => o.ExceptionHandlePolicy),
                new Func<ExceptionHandlePolicy, string>(o => o.Name));
        }

        private static void WriteInstrumentationConfiguration()
        {
            Save<InstrumentationConfiguration>("Instrumentation",
                Path.Combine(configXMLPath, "InstrumentationConfiguration.xml"),
                "Instrumentation");
        }

        private static void WriteLoggerConfiguration()
        {
            Save<LoggerConfiguration>("Logging",
                Path.Combine(configXMLPath, "LoggerConfiguration.xml"),
                "Logging");
        }

        private static void WriteMomConfiguration()
        {
            Save<MessagingConfig>(
                 "MOM",
                 Path.Combine(configXMLPath, "MessagingConfiguration.xml"), "Config");
        }

        private static void WriteObjectBuilderConfig()
        {
            Save<ObjectBuilderConfig, ObjectBuilder>("ObjectBuilderConfig",
                Path.Combine(configXMLPath, "ObjectBuilderConfig.xml"),
                new Func<ObjectBuilderConfig, IEnumerable<ObjectBuilder>>(o => o.ObjectBuilderCollection),
                new Func<ObjectBuilder, string>(o => o.TypeName));
        }

        private static void WriteObjectProviderConfig()
        {
            Save<ObjectProviderConfig, ObjectProvider>("ObjectProviderConfig",
                Path.Combine(configXMLPath, "ObjectProviderConfig.xml"),
                new Func<ObjectProviderConfig, IEnumerable<ObjectProvider>>(o => o.ObjectProviderCollection),
                new Func<ObjectProvider, string>(o => o.TypeName));
        }

        private static void WriteQueryBuilderConfig()
        {
            Save<QueryBuilderConfig, QueryBuilder>("QueryBuilderConfig",
                Path.Combine(configXMLPath, "QueryBuilderConfig.xml"),
                new Func<QueryBuilderConfig, IEnumerable<QueryBuilder>>(o => o.QueryBuilderCollection),
                new Func<QueryBuilder, string>(o => o.TypeName));
        }

        private static void WriteTraceConfiguration()
        {
            Save<TraceConfiguration>("Trace", Path.Combine(configXMLPath, "TraceConfiguration.xml"), "Trace");
        }

        private static void WritetypeShardConfigurationSection()
        {
            Save<TypeShardConfigurationSection, TypeShardElement>("Data.TypeShards",
                Path.Combine(configXMLPath, "typeShardConfigurationSection.xml"),
                new Func<TypeShardConfigurationSection, IEnumerable<TypeShardElement>>(o => o.TypeShardElement),
                new Func<TypeShardElement, string>(o => o.TypeName));
        }

        private static void WriteWorkFlowXmlConfigurations()
        {
            Save<Orchestration, WorkflowConfiguration>("Orchestration",
                Path.Combine(configXMLPath, "Orchestration.xml"),
                new Func<Orchestration, IEnumerable<WorkflowConfiguration>>(o => o.WorkflowConfigurations),
                new Func<WorkflowConfiguration, string>(o => o.Name));
        }

        private static void WriteWorkItemTypeConfiguration()
        {
            Save<WorkItemTypeConfiguration, WorkItemConfig>("WorkItemTypeConfiguration",
                Path.Combine(configXMLPath, "WorkItemTypeConfiguration.xml"),
                new Func<WorkItemTypeConfiguration, IEnumerable<WorkItemConfig>>(o => o.WorkItemConfig),
                new Func<WorkItemConfig, string>(o => o.Name));
        }

        private static void WriteEventBusSystemConfiguration()
        {
            Save<EventBusSystemConfiguration>("EventBusSystemConfiguration",
                Path.Combine(configXMLPath, "EventBusSystemConfiguration.xml"),
                "EventBusSystemConfiguration");
        }

        private static void WriteEventBusMessageTypeConfiguration()
        {
            Save<EventBusMessageTypeConfiguration>("EventBusMessageTypeConfiguration",
                Path.Combine(configXMLPath, "EventBusMessageTypeConfiguration.xml"),
                "EventBusMessageTypeConfiguration");
        }

        private static void WriteEventBusProviderConfiguration()
        {
            Save<EventBusProviderConfiguration>("EventBusProviderConfiguration",
                Path.Combine(configXMLPath, "EventBusProviderConfiguration.xml"),
                "EventBusProviderConfiguration");
        }

        private static void WriteEventBusProducerConfiguration()
        {
            Save<EventBusProducerConfiguration>("EventBusProducerConfiguration",
                Path.Combine(configXMLPath, "EventBusProducerConfiguration.xml"),
                "EventBusProducerConfiguration");
        }

        private static void WriteEventBusConsumerConfiguration()
        {
            Save<EventBusConsumerConfiguration>("EventBusConsumerConfiguration",
                Path.Combine(configXMLPath, "EventBusConsumerConfiguration.xml"),
                "EventBusConsumerConfiguration");
        }

        private static void WriteNumberGeneratorConfig()
        {
            Save<NumberGeneratorConfiguration, NumberGeneratorConfig>("NumberGeneratorConfig", Path.Combine(configXMLPath, "NumberGeneratorConfig.xml"),
                new Func<NumberGeneratorConfiguration, IEnumerable<NumberGeneratorConfig>>(o => o.NumberGeneratorConfigurations),
                new Func<NumberGeneratorConfig, string>(o => o.Key));
        }

        private static void WriteExternalStorageProviderConfig()
        {
            Save<ExternalStorageProviderConfiguration, ExternalStorageProviderConfig>("ExternalStorageProviderConfig", Path.Combine(configXMLPath, "ExternalStorageProviderConfig.xml"),
            new Func<ExternalStorageProviderConfiguration, IEnumerable<ExternalStorageProviderConfig>>(o => o.Providers),
            new Func<ExternalStorageProviderConfig, string>(o => o.Key));
        }
        #endregion Private Methods
    }
}