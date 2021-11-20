using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToUnity_DataConverter.Entities
{
    [Serializable]
    public class BusinessEntity
    {
        public int id;
        public int worldId;
        public string name;

        public float priceModifier;
        public string basePrice;
        public float profitSpeed;
        public string profit;

        public int state;
    }
}
