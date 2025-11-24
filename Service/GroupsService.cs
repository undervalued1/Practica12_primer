using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WpfApp1.Classes;
using WpfApp1.Data;

namespace WpfApp1.Service
{
    public class GroupsService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public static ObservableCollection<Group> Groups { get; set; } = new();
        public void GetAll()
        {
            var groups = _db.Groups.ToList();
            Groups.Clear();
            foreach (var group in groups)
                Groups.Add(group);
        }
        public int Commit() => _db.SaveChanges();
        public GroupsService()
        {
            GetAll();
        }
        public void Add(Group group)
        {
            var _group = new Group
            {
                Title = group.Title,
            };
            _db.Add<Group>(_group);
            Commit();
            Groups.Add(_group);
        }
        public void Remove(Group group)
        {
            _db.Remove<Group>(group);
            if (Commit() > 0)
                if (Groups.Contains(group))
                    Groups.Remove(group);
        }
    }
}
