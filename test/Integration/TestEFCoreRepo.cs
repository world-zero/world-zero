using System.Linq;
using System.Collections.ObjectModel;
using WorldZero.Common.Interface;
using WorldZero.Data.Model;
using WorldZero.Data;
using WorldZero.Data.Repository;
using NUnit.Framework;
using System;

// NOTE: These test methods are not independent of one another.
// Also the annoying letters at the start of each test is to make sure that
// nothing attempts to run these tests non-serially.

namespace WorldZero.Test.Integration
{
    // TODO: be sure not to push your credentials
    [TestFixture]
    public class TestEFCoreRepo
    {
        private W0Context _context;
        private EFCoreRepo<Player> _repo;
        private ObservableCollection<Player> _local;
        private string _contextString;

        private EFCoreRepo<Player> _newRepoConnection(W0Context context=null)
        {
            if (context == null)
                return new EFCoreRepo<Player>(new W0Context(this._contextString));
            else
                return new EFCoreRepo<Player>(context);
        }

        [OneTimeSetUp]
        public void ResetDb()
        {
            // TODO: I am aware that this is poor and I'm fixing it later.
            this._contextString = "Data Source=.;Server=localhost;Database=W0Test;User Id=fake;Password=extrafake;";
            this._context = new W0Context(this._contextString);
            this._context.Database.Delete();
            this._repo = new EFCoreRepo<Player>(this._context);
            this._local = this._context.Set<Player>().Local;
        }

        [Test]
        public void a_TestConstructor()
        {
            Assert.Throws<ArgumentException>(()=>new EFCoreRepo<Player>(null));
            Assert.Throws<ArgumentException>(()=>new EFCoreRepo<BadModel>(this._context));
        }

        [Test]
        // Create p0 and p1, both from different repo connections.
        public void b_TestInsertSave()
        {
            var p0 = new Player()
            {
                Username = "Jack"
            };
            this._repo.Insert(p0);
            Assert.IsTrue(this._local.Contains(p0));
            Assert.AreEqual(p0.PlayerId, 0);

            this._repo.Save();
            Assert.AreEqual(1, p0.PlayerId);

            var _otherRepoConnection = this._newRepoConnection();
            var p1 = _otherRepoConnection.GetById(1);
            Assert.AreEqual(p0.PlayerId, p1.PlayerId);
            Assert.AreEqual(p0.Username, p1.Username);
            Assert.AreEqual(p0.IsBlocked, p1.IsBlocked);
        }

        [Test]
        // Update p0 and see that the changes persist to the other repo after
        // an update occurs.
        public void c_TestUpdateSave()
        {
            var p0 = this._repo.GetById(1);
            p0.Username = "Hal";
            this._repo.Update(p0);
            this._repo.Save();
            var _otherRepoConnection = this._newRepoConnection();
            var p1 = _otherRepoConnection.GetById(1);
            Assert.AreEqual(p0.Username, p1.Username);
        }

        [Test]
        // No happy test is done as it is used earlier in this file.
        public void d_TestGetByIdSadBad()
        {
            Assert.IsNull(this._repo.GetById(-1));
            Assert.Throws<ArgumentException>(()=>this._repo.GetById("Puppy"));
        }

        [Test]
        public void e_TestGetAll()
        {
            var p0 = new Player()
            {
                Username = "Chair",
                IsBlocked = true
            };
            this._repo.Insert(p0);
            this._repo.Save();
            var results = this._repo.GetAll();
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void f_TestDelete()
        {
            this._repo.Delete(1);
            this._repo.Save();
            var results = this._repo.GetAll();
            Assert.AreEqual(1, results.Count());
        }
    }

    public class BadModel : IModel { }
}