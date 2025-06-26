using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class MockSupervisorRepository : ISupervisorRepository
    {
        private List<Supervisor> _supervisorList;
        public Supervisor GetSupervisor(int Id)
        {
            return _supervisorList.FirstOrDefault(e => e.Id == Id);
        }

        //public Supervisor GetSupervisorByClass(int classId)
        //{
        //    return _SupervisorList.FirstOrDefault(e => e.Class == classId);
        //}
        //public int GetSupervisorClass(int Id)
        //{

        //}

        public IEnumerable<Supervisor> GetAllSupervisors()
        {
            return _supervisorList;
        }

        public IEnumerable<Supervisor> GetSupervisorByUserId(string userId)
        {
            // Call the repository method
            return _supervisorList
                .Where(s => s.UserId == userId)
                .ToList();
        }

        public Supervisor Add(Supervisor supervisor)
        {
            supervisor.Id = _supervisorList.Max(e => e.Id) + 1;
           supervisor.UserId = supervisor.ApplicationUser.Id;
            _supervisorList.Add(supervisor);
            return supervisor;
        }

        public Supervisor Update(Supervisor supervisorChanges)
        {
            Supervisor supervisor = _supervisorList.FirstOrDefault(e => e.Id == supervisorChanges.Id);
            if (supervisor != null)
            {
                supervisor.Name = supervisorChanges.Name;
                supervisor.Email = supervisorChanges.Email;
                supervisor.Gender = supervisorChanges.Gender;
            }
            return supervisor;
        }

        public Supervisor Delete(int id)
        {
            Supervisor supervisor = _supervisorList.FirstOrDefault(e => e.Id == id);
            if (supervisor != null)
            {
                _supervisorList.Remove(supervisor);
            }
            return supervisor;
        }

        public MockSupervisorRepository()
        {
            _supervisorList = new List<Supervisor>
            {
                new Supervisor(){Id=1, Name="Sarah", Email="sarah@pragim", CNIC = "12345-6789123-4", Gender = GenderType.Female, Address="GrooveStreet"},
                new Supervisor(){Id=2, Name="James", Email="james@pragim", CNIC = "12345-6789123-5", Gender = GenderType.Male, Address="ViceCity"},
                new Supervisor(){Id=3, Name="Peter", Email="peter@pragim", CNIC = "12345-6789123-6", Gender = GenderType.Male, Address="DownTown"}
            };
        }
    }
}
