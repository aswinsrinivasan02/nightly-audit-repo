using BallyTech.Infrastructure.Data;
using System;

namespace SG.NightlyAudit.Utilities
{
    public class ShardHashProvider : IShardHashProvider
    {
        Int32 IShardHashProvider.Compute<TId>(TId id)
        {
            return 1;
        }
    }
}
