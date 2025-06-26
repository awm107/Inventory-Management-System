using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class MockSalesOfficerRepository : ISalesOfficerRepository
    {
        private List<SalesOfficer> _SalesOfficerList;
        public SalesOfficer GetSalesOfficer(int Id)
        {
            return _SalesOfficerList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<SalesOfficer> GetAllSalesOfficers()
        {
            return _SalesOfficerList;
        }

        //public IEnumerable<SalesOfficer> GetAllSalesOfficersByClass(uint classId)
        //{
        //    return (IEnumerable<SalesOfficer>)_SalesOfficerList.Where(e => e.Class == classId).ToList();
        //}
        public IEnumerable<SalesOfficer> GetSalesOfficersByUserId(string userId)
        {
            // Call the repository method
            return _SalesOfficerList
                .Where(s => s.UserId == userId)
                .ToList();
        }

        public SalesOfficer Add(SalesOfficer SalesOfficer)
        {
            SalesOfficer.Id = _SalesOfficerList.Max(e => e.Id) + 1;
            SalesOfficer.UserId = SalesOfficer.ApplicationUser.Id;
            _SalesOfficerList.Add(SalesOfficer);
            return SalesOfficer;
        }

        public SalesOfficer Update(SalesOfficer SalesOfficerChanges)
        {
            SalesOfficer SalesOfficer = _SalesOfficerList.FirstOrDefault(e => e.Id == SalesOfficerChanges.Id);
            if (SalesOfficer != null)
            {
                SalesOfficer.Name = SalesOfficerChanges.Name;
                SalesOfficer.Email = SalesOfficerChanges.Email;
                SalesOfficer.Gender = SalesOfficerChanges.Gender;
            }
            return SalesOfficer;
        }

        public SalesOfficer Delete(int id)
        {
            SalesOfficer SalesOfficer = _SalesOfficerList.FirstOrDefault(e => e.Id == id);
            if (SalesOfficer != null)
            {
                _SalesOfficerList.Remove(SalesOfficer);
            }
            return SalesOfficer;
        }

        public MockSalesOfficerRepository()
        {
            _SalesOfficerList = new List<SalesOfficer>
            {
                new SalesOfficer(){Id=1, Name="Mary", Email="mary@pragim", CNIC = "12345-6789123-1", Gender = GenderType.Female, Address="LA"},
                new SalesOfficer(){Id=2, Name="John", Email="john@pragim", CNIC = "12345-6789123-2", Gender = GenderType.Male, Address="ViceCity"},
                new SalesOfficer(){Id=3, Name="Sam", Email="sam@pragim", CNIC = "12345-6789123-3", Gender = GenderType.Male, Address="LibertyCity"}
            };
        }
    }
}
