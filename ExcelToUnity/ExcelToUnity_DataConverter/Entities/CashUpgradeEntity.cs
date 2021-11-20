using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToUnity_DataConverter.Entities
{
    [System.Serializable]
    public class SoulUpgrade
    {
        public int id;

        public float rewardablePrice;
        public int powPrice;
        public int rewardType;
        public float rewardAmount;
        public int rewardId;
        public int businessGetReward;
    }
}
