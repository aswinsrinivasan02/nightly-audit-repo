using BallyTech.Infrastructure.IDGeneration;
using BallyTech.Infrastructure.Types;
using SG.NightlyAudit.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.NightlyAudit.Persistance
{
    public static class Configurator
    {
        public static void Configure(DIContainer diContainer, BallyTech.Infrastructure.Provider.IServiceProvider serviceProvider)
        {
            diContainer.Register<IIDRangeRepository>(new IDRangeRepository(serviceProvider));
            diContainer.Register<IAuditHistoryRepository>(new AuditHistoryRepository(serviceProvider));
            diContainer.Register<IProductRepository>(new ProductRepository(serviceProvider));
            diContainer.Register<IAuditJobRepository>(new AuditJobRepository(serviceProvider));
            diContainer.Register<IJobRepository>(new JobRepository(serviceProvider));

            diContainer.Register<IRangeGenerator<int>>(new RangeGenerator<int>(diContainer.Get<IIDRangeRepository>()));
            diContainer.Register<IRangeGenerator<long>>(new RangeGenerator<long>(diContainer.Get<IIDRangeRepository>()));
            diContainer.Register<IRangeGenerator<short>>(new RangeGenerator<short>(diContainer.Get<IIDRangeRepository>()));
        }
    }
}
