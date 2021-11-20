using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToUnity_DataConverter.Entities
{
    [Serializable]
    public class BusinessGoalEntity
    {
        public int id;
        public int businessId;
        public int required;
        public string name;
        public int rewardType;
        public int rewardAmount;
        public int rewardId;
        public int businessGetReward;
        public int worldId;
    }
}
