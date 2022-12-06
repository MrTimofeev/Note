using System;
using System.Collections.Generic;
using System.Text;

namespace Note.Model
{
    public class TaskGroupModel : List<TaskModel>
    {
        public string Name { get; private set; }

        public TaskGroupModel(string name, List<TaskModel> tasks) : base(tasks)
        {
            Name = name;
        }
    }
}
