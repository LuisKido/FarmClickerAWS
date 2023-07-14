using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmClicker.Models
{
    public class Farm
    {
        [DynamoDBHashKey] // Esto denota que este atributo es tu clave de partición
        public string UserId { get; set; }

        [DynamoDBRangeKey] // Esto denota que este atributo es tu clave de clasificación
        public string FarmId { get; set; }
        public string FarmType { get; set; } // "Wheat", "Corn", "Rice", etc.
        public int Level { get; set; }
        public int ProducePerClick { get; set; }
        public int ProducePerSecond { get; set; }
        public int ExperiencePerHarvest { get; set; }
        public int UpgradeCost { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CurrentScore { get; set; }
    }
}
