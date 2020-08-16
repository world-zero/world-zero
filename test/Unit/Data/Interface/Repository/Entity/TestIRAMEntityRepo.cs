using System.Linq;
using System;
using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM
{
    // NOTE: These tests do not test functional member `GenerateId`, and that
    // is done in TestIRAMIdEntityRepo.cs .
    [TestFixture]
    public class TestIRAMEntityRepo
    {
        private Name _eraName;
        private Era _era;
        private Era[] _eras;
        private HashSet<Era> _storedEras;
        private TestRAMEntityRepo _repo;

        private void _assertErasEqual(Era expected, Era actual)
        {
            Console.WriteLine($"Expected {expected.Id.Get}, got {actual.Id.Get}");
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
        }

        [SetUp]
        public void Setup()
        {
            this._eraName = new Name("Jack's Tyranny");
            this._era = new Era(this._eraName, new PastDate(DateTime.UtcNow));
            this._repo = new TestRAMEntityRepo();

            // These are used not for checking their specific values, but for
            // checking that they are being CRUD-ed correctly. This is also why
            // _storedEras exists.
            this._eras    = new Era[5];
            this._eras[0] = new Era(new Name("There was darkness"), new PastDate(DateTime.UtcNow));
            this._eras[1] = new Era(new Name("And then there was Jack"), new PastDate(DateTime.UtcNow));
            this._eras[2] = new Era(new Name("His reign was supreme and teethy"), new PastDate(DateTime.UtcNow));
            this._eras[3] = new Era(new Name("Until a challenger, Hal, approached"), new PastDate(DateTime.UtcNow));
            this._eras[4] = new Era(new Name("And promptly got destroyed"), new PastDate(DateTime.UtcNow));
            this._storedEras = new HashSet<Era>();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(this._repo.Saved);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.IsNotNull(this._repo.Staged);
            Assert.AreEqual(0, this._repo.Staged.Count);
        }

        [Test]
        public void TestInsertSave()
        {
            this._repo.Insert(this._era);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            Assert.AreEqual(this._eraName, this._repo.Staged[this._eraName].Id);

            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            Assert.AreEqual(this._eraName, this._repo.Saved[this._eraName].Id);
        }

        [Test]
        public void TestUpdate()
        {
            Assert.IsNull(this._era.EndDate);
            this._repo.Insert(this._era);
            this._repo.Save();

            this._era.EndDate = new PastDate(DateTime.UtcNow);
            this._repo.Update(this._era);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);

            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            Assert.IsNotNull(this._era.EndDate);
        }

        [Test]
        public void TestDelete()
        {
            this._repo.Insert(this._era);
            this._repo.Save();

            this._repo.Delete(this._era.Id);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            Assert.IsNull(this._repo.Staged[this._era.Id]);

            this._repo.Save();
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);

            var newEra = new Era(new Name("new"), new PastDate(DateTime.UtcNow));
            this._repo.Insert(newEra);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            Assert.AreEqual(this._repo.Staged[newEra.Id], newEra);
            this._repo.Delete(newEra.Id);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
        }

        [Test]
        public void TestGetById()
        {
            this._repo.Insert(this._era);
            this._repo.Save();

            var era = this._repo.GetById(this._era.Id);
            this._assertErasEqual(this._era, era);

            Assert.Throws<ArgumentException>(()=>this._repo.GetById(new Name("FAKKKKEEE")));
        }

        [Test]
        public void TestGetAll()
        {
            foreach (Era e in this._eras)
                this._repo.Insert(e);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(5, this._repo.Staged.Count);

            this._repo.Save();
            Assert.AreEqual(5, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);

            HashSet<Era> newEras = this._repo.GetAll().ToHashSet();
            Assert.AreEqual(this._eras.Length, newEras.Count);
        }

        [Test]
        public void TestSequenceOfActions()
        {
            this._repo.Insert(this._eras[0]);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._storedEras.Add(this._eras[0]);

            this._repo.Insert(this._eras[1]);
            this._repo.Insert(this._eras[2]);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(2, this._repo.Staged.Count);
            this._storedEras.Add(this._eras[1]);
            this._storedEras.Add(this._eras[2]);

            this._repo.Save();
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);

            this._repo.Delete(this._eras[4].Id);
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);

            this._eras[0].EndDate = new PastDate(DateTime.UtcNow);
            this._repo.Update(this._eras[0]);
            this._repo.Delete(this._eras[1].Id);
            this._repo.Insert(this._eras[3]);
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(4, this._repo.Staged.Count);

            this._repo.Save();
            this._storedEras.Remove(this._eras[1]);
            this._storedEras.Add(this._eras[3]);
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            HashSet<Era> newEras = this._repo.GetAll().ToHashSet();
            Assert.AreEqual(3, newEras.Count);
        }

        [Test]
        public void TestSadDelete()
        {
            this._repo.Delete(this._eras[0].Id);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
        }
    }

    public class TestRAMEntityRepo
        : IRAMEntityRepo<Era, Name, string>
    {
        public TestRAMEntityRepo()
            : base()
        { }

        public Dictionary<Name, Era> Saved { get { return this._saved; } }
        public Dictionary<Name, Era> Staged { get { return this._staged; }}
    }
}