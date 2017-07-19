using BallyTech.Infrastructure.Data;
using BallyTech.Infrastructure.IDGeneration;
using BallyTech.Infrastructure.Provider;

namespace SG.NightlyAudit.Persistance
{
    public partial class IDRangeRepository : Repository<IDRange, IDRangeId>, IIDRangeRepository
    {
        public IDRangeRepository(IServiceProvider serviceProvider)
            : base(serviceProvider.Data.GetStorage())
        {
        }

        IDRange IIDRangeRepository.GetNextRange(string key)
        {
            return this.FindBy(new IDRangeId(key));
        }

        public override IDRangeId NextID()
        {
            return null;
        }
    }
}
