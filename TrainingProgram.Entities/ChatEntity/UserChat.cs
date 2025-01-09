using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProgram.Entities.IEntity;

namespace TrainingProgram.Entities.ChatEntity
{
    public class UserChat : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool isOnline { get; set; }
    }
}
