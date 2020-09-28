using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Collections;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM
{
    // NOTE: For readability, tests that test basic, non-rule related
    // functionality are their own test methods, while rule-related tests are
    // separated.
    // NOTE: These tests do not test function member `GenerateId`, and that is
    // done in TestIRAMIdEntityRepo.cs .
    // NOTE: Since DummyEntity can only have set Ids, these tests do not make
    // sure that Ids are not artifacts in entities that are "saved" before a
    // failed save. These tests are performed in TestIRAMIdEntityRepo.

    [TestFixture]
    public class TestIRAMEntityRepo
    {
        private Name _name;
        private int _unique;
        private int _combo0;
        private int _combo1;
        private DummyEntity _e;
        private DummyEntity[] _entities;
        private W0Set<DummyEntity> _storedEntities;
        private TestRAMEntityRepo _repo;

        private void
        _assertEntitiesEqual(DummyEntity expected, DummyEntity actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Unique, actual.Unique);
            Assert.AreEqual(expected.Combo0, actual.Combo0);
            Assert.AreEqual(expected.Combo1, actual.Combo1);
        }

        private void _assertUniformRuleCounts(int uniform)
        {
            this._assertUniformRuleCounts(uniform, uniform, uniform);
        }

        private void _assertUniformRuleCounts(int saved, int staged, int recycled=0)
        {
            for (int i = 0; i < this._repo.SavedRules.Count; i++)
            {
                Assert.AreEqual(saved, this._repo.SavedRules[i].Count);
                Assert.AreEqual(staged, this._repo.StagedRules[i].Count);
                Assert.AreEqual(recycled, this._repo.RecycledRules[i].Count);
            }
        }

        /// <summary>
        /// Make sure that the supplied entity's rules are or are not present
        /// in the supplied list of dicts of rules. If they should be present
        /// and are, then make sure that they contain the correct entity.
        /// </summary>
        private void _assertRulesStored(
            DummyEntity e,
            List<Dictionary<W0Set<object>, DummyEntity>> storedRules,
            bool shouldBePresent,
            bool shouldBeNull=false
        )
        {
            var rules = e.GetUniqueRules();
            for (int i = 0; i < rules.Count; i++)
            {
                var rule = rules[i];
                bool containsRule = storedRules[i].ContainsKey(rule);
                Assert.AreEqual(shouldBePresent, containsRule);
                if (shouldBeNull)
                    Assert.IsNull(storedRules[i][rule]);
                else if (containsRule)
                {
                    Assert.IsNotNull(storedRules[i][rule]);
                    this._assertEntitiesEqual(e, storedRules[i][rule]);
                }
            }
        }

        [SetUp]
        public void Setup()
        {
            this._name = new Name("foo");
            this._unique = 2;
            this._combo0 = 39;
            this._combo1 = 93;
            this._e = new DummyEntity(
                this._name,
                this._unique,
                this._combo0,
                this._combo1
            );
            this._repo = new TestRAMEntityRepo();

            // These are used not for checking their specific values, but for
            // checking that they are being CRUD-ed correctly. This is also why
            // _storedEntities exists.
            this._entities    = new DummyEntity[5];
            this._entities[0] = new DummyEntity(new Name("foo"), 1, 8, 6);
            this._entities[1] = new DummyEntity(new Name("bar"), 4, 0, 3);
            this._entities[2] = new DummyEntity(new Name("baz"), 9, 0, 5);
            this._entities[3] = new DummyEntity(new Name("oof"), 3, 7, 3);
            this._entities[4] = new DummyEntity(new Name("zab"), 0, 3, 9);
            this._storedEntities = new W0Set<DummyEntity>();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(this._repo.Saved);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.IsNotNull(this._repo.Staged);
            Assert.AreEqual(0, this._repo.Staged.Count);
            Assert.IsNotNull(this._repo.SavedRules);
            Assert.IsNotNull(this._repo.SavedRules);
            Assert.IsNotNull(this._repo.StagedRules);
            Assert.IsNotNull(this._repo.RecycledRules);
            this._assertUniformRuleCounts(0);
        }

        [Test]
        public void TestGetById()
        {
            this._repo.Insert(this._e);
            this._repo.Save();

            var entity = this._repo.GetById(this._e.Id);
            this._assertEntitiesEqual(this._e, entity);

            Assert.Throws<ArgumentException>(()=>this._repo.GetById(new Name("FAKKKKEEE")));
        }

        [Test]
        public void TestGetAll()
        {
            foreach (DummyEntity e in this._entities)
                this._repo.Insert(e);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(5, this._repo.Staged.Count);

            this._repo.Save();
            Assert.AreEqual(5, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);

            W0Set<DummyEntity> newEntities =
                new W0Set<DummyEntity>(this._repo.GetAll().ToHashSet());
            Assert.AreEqual(this._entities.Length, newEntities.Count);
        }

        [Test]
        public void TestInsertSave()
        {
            Assert.Throws<ArgumentNullException>(()=>this._repo.Insert(null));

            this._repo.Insert(this._e);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            Assert.AreEqual(this._name, this._repo.Staged[this._name].Id);

            Assert.Throws<ArgumentException>(()=>this._repo.Insert(
                new DummyEntity(this._name, 34, 43224, 432344)
            ));

            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._assertUniformRuleCounts(1, 0);
            Assert.AreEqual(this._name, this._repo.Saved[this._name].Id);
        }

        [Test]
        public void TestInsertSaveRules()
        {
            this._assertUniformRuleCounts(0);
            var rules = this._e.GetUniqueRules();
            this._repo.Insert(this._e);
            this._assertUniformRuleCounts(0, 1);
            this._assertRulesStored(this._e, this._repo.SavedRules, false);
            this._assertRulesStored(this._e, this._repo.StagedRules, true);

            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
            this._assertRulesStored(this._e, this._repo.SavedRules, true);
            this._assertRulesStored(this._e, this._repo.StagedRules, false);

            // Make sure that Save() uses Discard() on failure.
            this._repo.Insert(new DummyEntity(new Name("Valid"), 999, -1, -4));
            this._repo.Insert(
                new DummyEntity(new Name("roommate"), this._unique, 432, 3244)
            );
            Assert.Throws<ArgumentException>(()=>this._repo.Save());
            this._assertUniformRuleCounts(1, 0);
            this._assertRulesStored(this._e, this._repo.SavedRules, true);
            this._assertRulesStored(this._e, this._repo.StagedRules, false);
        }

        [Test]
        public void TestUpdate()
        {
            Assert.AreEqual(this._unique, this._e.Unique);
            this._repo.Insert(this._e);
            this._repo.Save();

            var newInt = this._unique + 5;
            this._e.Unique = newInt;
            this._repo.Update(this._e);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);

            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            Assert.AreEqual(newInt, this._e.Unique);
        }

        [Test]
        public void TestUpdateSad()
        {
            this._repo.Insert(this._e);
            var e = new DummyEntity(new Name("roommate"), 99, 432, 3244);
            this._repo.Insert(e);
            this._repo.Save();
            var bad = 999;
            this._e.Unique = bad;
            e.Unique = bad;
            this._repo.Update(this._e);
            Assert.Throws<ArgumentException>(()=>this._repo.Update(e));
        }

        [Test]
        public void TestUpdateNoRuleChanges()
        {
            this._repo.Insert(this._e);
            this._repo.Save();

            this._e.Other = -432;
            this._repo.Update(this._e);
            this._assertUniformRuleCounts(1, 0);
            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
        }

        [Test]
        public void TestUpdatePartialRuleChange()
        {
            this._repo.Insert(this._e);
            this._repo.Save();

            var oldRules = this._e.GetUniqueRules();
            this._e.Combo1 = -432;
            var newRules = this._e.GetUniqueRules();

            this._repo.Update(this._e);
            for (int i = 0; i < this._repo.SavedRules.Count; i++)
                Assert.AreEqual(1, this._repo.SavedRules[i].Count);
            Assert.AreEqual(0, this._repo.StagedRules[0].Count);
            Assert.AreEqual(2, this._repo.StagedRules[1].Count);
            Assert.IsNull(this._repo.StagedRules[1][oldRules[1]]);
            this._assertEntitiesEqual(
                this._e, this._repo.StagedRules[1][newRules[1]]);

            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
        }

        [Test]
        public void TestUpdateFullRuleChange()
        {
            this._repo.Insert(this._e);
            this._repo.Save();

            var oldRules = this._e.GetUniqueRules();
            this._e.Unique = -432;
            var newRules = this._e.GetUniqueRules();

            this._repo.Update(this._e);
            for (int i = 0; i < this._repo.SavedRules.Count; i++)
                Assert.AreEqual(1, this._repo.SavedRules[i].Count);
            Assert.AreEqual(2, this._repo.StagedRules[0].Count);
            Assert.AreEqual(0, this._repo.StagedRules[1].Count);
            Assert.IsNull(this._repo.StagedRules[0][oldRules[0]]);
            this._assertEntitiesEqual(
                this._e, this._repo.StagedRules[0][newRules[0]]);

            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
        }

        [Test]
        public void TestDeleteInvalidId()
        {
            this._repo.Delete(new Name("key smash"));
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            this._assertUniformRuleCounts(0);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._assertUniformRuleCounts(0);
        }

        [Test]
        public void TestDelete()
        {
            this._repo.Insert(this._e);
            this._repo.Save();

            this._repo.Delete(this._e.Id);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            Assert.IsNull(this._repo.Staged[this._e.Id]);

            this._repo.Save();
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
        }

        [Test]
        public void TestDeleteRule()
        {
            var rules = this._e.GetUniqueRules();
            this._repo.Insert(this._e);
            this._repo.Save();

            this._repo.Delete(this._e.Id);
            this._assertUniformRuleCounts(1, 1);
            for (int i = 0; i < rules.Count; i++)
                Assert.IsNull(this._repo.StagedRules[i][rules[i]]);

            this._repo.Save();
            this._assertUniformRuleCounts(0, 0);
        }

        [Test]
        public void TestDeleteStagedOnlyEntity()
        {
            var newEntity = new DummyEntity(new Name("new"), 3, 6, 9);
            this._repo.Insert(newEntity);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            this._assertEntitiesEqual(
                newEntity, this._repo.Staged[newEntity.Id]);
            // ^^ Not tested heavily as covered by this.TestInsertSave() and
            // this.TestInsertSaveRules().

            this._repo.Delete(newEntity.Id);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            this._assertUniformRuleCounts(0, 1);
            var rules = newEntity.GetUniqueRules();
            for (int i = 0; i < rules.Count; i++)
                Assert.IsNull(this._repo.StagedRules[i][ rules[i] ]);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
        }

        [Test]
        public void TestDeleteStagedEntityWithSavedRule()
        // What if a staged-only entity is deleted that has a rule that is
        // saved-only?
        {
            this._assertUniformRuleCounts(0);
            var rules = this._e.GetUniqueRules();
            this._repo.Insert(this._e);
            this._assertUniformRuleCounts(0, 1);
            this._assertRulesStored(this._e, this._repo.SavedRules, false);
            this._assertRulesStored(this._e, this._repo.StagedRules, true);

            var newDumy = new DummyEntity(new Name("fka"), this._unique, 1, 0);
            this._repo.Insert(newDumy);
            this._repo.Delete(newDumy.Id);
            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
            this._assertRulesStored(this._e, this._repo.SavedRules, true);
            this._assertRulesStored(this._e, this._repo.StagedRules, false);
        }

        [Test]
        public void TestRecycledRules()
        {
            this._repo.Insert(this._e);
            this._repo.Save();
            this._repo.Delete(this._e.Id);

            var newDummy = new DummyEntity(new Name("fd"), this._unique, 3, 4);
            this._repo.Insert(newDummy);
            Assert.AreEqual(1, this._repo.RecycledRules[0].Count);
            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
            var rules = newDummy.GetUniqueRules();
            for (int i = 0; i < rules.Count; i++)
            {
                this._assertEntitiesEqual(
                    newDummy, this._repo.SavedRules[i][ rules[i] ]);
            }
        }

        [Test]
        public void TestRecycledRulesTwice()
        {
            this._repo.Insert(this._e);
            this._repo.Save();
            int unq = 9;
            int combo0 = 3;
            int combo1 = 4;
            var e = new DummyEntity(new Name("fd"), unq, combo0, combo1);
            this._repo.Insert(e);
            this._repo.Save();
            this._assertUniformRuleCounts(2, 0);
            this._assertRulesStored(this._e, this._repo.SavedRules, true);
            this._assertRulesStored(e, this._repo.SavedRules, true);

            this._repo.Delete(this._e.Id);
            this._repo.Delete(e.Id);
            this._assertUniformRuleCounts(2, 2);
            this._assertRulesStored(this._e,this._repo.StagedRules,true,true);
            this._assertRulesStored(e, this._repo.StagedRules, true, true);

            var a = new DummyEntity(
                new Name("a"), this._unique, this._combo0, this._combo1);
            var b = new DummyEntity(new Name("b"), unq, combo0, combo1);
            this._repo.Insert(a);
            this._repo.Insert(b);
            this._assertUniformRuleCounts(2, 2, 2);
            this._assertRulesStored(a, this._repo.StagedRules, true);
            this._assertRulesStored(b, this._repo.StagedRules, true);

            this._repo.Save();
            this._assertUniformRuleCounts(2, 0);
            this._assertRulesStored(a, this._repo.SavedRules, true);
            this._assertRulesStored(b, this._repo.SavedRules, true);
        }

        [Test]
        public void TestDiscard()
        {
            this._assertUniformRuleCounts(0);
            var rules = this._e.GetUniqueRules();
            this._repo.Insert(this._e);
            this._repo.Save();
            this._assertUniformRuleCounts(1, 0);
            this._assertRulesStored(this._e, this._repo.SavedRules, true);
            this._assertRulesStored(this._e, this._repo.StagedRules, false);

            this._repo.Delete(this._e.Id);
            this._repo.Insert(
                new DummyEntity(new Name("x"), this._unique, 3, 4));

            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(2, this._repo.Staged.Count);
            Assert.AreEqual(1, this._repo.RecycledRules[0].Count);
            Assert.IsNull(this._repo.Staged[this._e.Id]);

            this._repo.Discard();
            Assert.IsNotNull(this._repo.Staged);
            Assert.AreEqual(0, this._repo.Staged.Count);
            Assert.IsNotNull(this._repo.StagedRules);
            Assert.IsNotNull(this._repo.RecycledRules);
            this._assertUniformRuleCounts(1, 0);
            this._assertRulesStored(this._e, this._repo.SavedRules, true);
            this._assertRulesStored(this._e, this._repo.StagedRules, false);
        }

        [Test]
        public void TestSequenceOfActions()
        {
            this._repo.Insert(this._entities[0]);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._storedEntities.Add(this._entities[0]);
            this._assertUniformRuleCounts(1, 0);
            this._assertRulesStored(
                this._entities[0], this._repo.SavedRules, true);
            this._assertRulesStored(
                this._entities[0], this._repo.StagedRules, false);

            this._repo.Insert(this._entities[1]);
            this._repo.Insert(this._entities[2]);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(2, this._repo.Staged.Count);
            this._storedEntities.Add(this._entities[1]);
            this._storedEntities.Add(this._entities[2]);
            this._assertUniformRuleCounts(1, 2);
            this._assertRulesStored(
                this._entities[1], this._repo.SavedRules, false);
            this._assertRulesStored(
                this._entities[1], this._repo.StagedRules, true);
            this._assertRulesStored(
                this._entities[2], this._repo.SavedRules, false);
            this._assertRulesStored(
                this._entities[2], this._repo.StagedRules, true);

            this._repo.Save();
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._assertUniformRuleCounts(3, 0);
            this._assertRulesStored(
                this._entities[1], this._repo.SavedRules, true);
            this._assertRulesStored(
                this._entities[1], this._repo.StagedRules, false);
            this._assertRulesStored(
                this._entities[2], this._repo.SavedRules, true);
            this._assertRulesStored(
                this._entities[2], this._repo.StagedRules, false);

            this._repo.Delete(this._entities[4].Id);
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            this._assertUniformRuleCounts(3, 0);

            this._entities[0].Unique = 90000;
            this._repo.Update(this._entities[0]);
            this._repo.Delete(this._entities[1].Id);
            this._repo.Insert(this._entities[3]);
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(4, this._repo.Staged.Count);

            this._repo.Save();
            this._storedEntities.Remove(this._entities[1]);
            this._storedEntities.Add(this._entities[3]);
            Assert.AreEqual(3, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            W0Set<DummyEntity> newEntities =
                new W0Set<DummyEntity>(this._repo.GetAll().ToHashSet());
            Assert.AreEqual(3, newEntities.Count);
        }
    }

    [TestFixture]
    public class TestDummyEntity
    {
        private Name _name;
        private int _unique;
        private int _combo0;
        private int _combo1;
        private int _other;
        private DummyEntity _e;

        [SetUp]
        public void Setup()
        {
            this._name = new Name("test");
            this._unique = 2;
            this._combo0 = 39;
            this._combo1 = 93;
            this._other = 342;
            this._e = new DummyEntity(
                this._name,
                this._unique,
                this._combo0,
                this._combo1,
                this._other
            );
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(this._name, this._e.Id);
            Assert.AreEqual(this._unique, this._e.Unique);
            Assert.AreEqual(this._combo0, this._e.Combo0);
            Assert.AreEqual(this._combo1, this._e.Combo1);
            Assert.AreEqual(this._other, this._e.Other);
        }

        [Test]
        public void TestClone()
        {
            var e = (DummyEntity) this._e.Clone();
            Assert.AreEqual(e.Unique, this._e.Unique);
            Assert.AreEqual(e.Combo0, this._e.Combo0);
            Assert.AreEqual(e.Combo1, this._e.Combo1);
            Assert.AreEqual(this._other, this._e.Other);
        }

        [Test]
        public void TestGetUniqueRules()
        {
            bool firstRulePresent = false;
            bool secondRulePresent = false;
            var rule0 = new W0Set<object>();
            rule0.Add(this._unique);
            var rule1 = new W0Set<object>();
            rule1.Add(this._combo0);
            rule1.Add(this._combo1);

            foreach (W0Set<object> rule in this._e.GetUniqueRules())
            {
                if (rule0.Equals(rule))
                    firstRulePresent = true;
                else if (rule1.Equals(rule))
                    secondRulePresent = true;
            }

            Assert.IsTrue(firstRulePresent);
            Assert.IsTrue(secondRulePresent);
        }
    }

    /// <summary>
    /// This is a dummy entity used to for testing with TestRAMEntityRepo. This
    /// class contains two unique rules, one single and one double.
    /// </summary>
    public class DummyEntity
        : INamedEntity
    {
        public int Unique { get; set; }
        public int Combo0 { get; set; }
        public int Combo1 { get; set; }
        public int Other { get; set; }

        public
        DummyEntity(Name id, int unique, int combo0, int combo1, int other=909)
            : base(id)
        {
            this.Unique = unique;
            this.Combo0 = combo0;
            this.Combo1 = combo1;
            this.Other = other;
        }

        internal override W0List<W0Set<object>> GetUniqueRules()
        {
            var r = base.GetUniqueRules();
            var rule0 = new W0Set<object>();
            rule0.Add(this.Unique);
            r.Add(rule0);
            var rule1 = new W0Set<object>();
            rule1.Add(this.Combo0);
            rule1.Add(this.Combo1);
            r.Add(rule1);
            return r;
        }

        public override IEntity<Name, string> Clone()
        {
            return new DummyEntity(
                this.Id,
                this.Unique,
                this.Combo0,
                this.Combo1,
                this.Other
            );
        }
    }

    public class TestRAMEntityRepo
        : IRAMEntityRepo<DummyEntity, Name, string>
    {
        public TestRAMEntityRepo()
            : base()
        { }

        protected override int GetRuleCount()
        {
            var a = new DummyEntity(new Name("foo"), 2, 3, 9);
            return a.GetUniqueRules().Count;
        }

        public Dictionary<Name, DummyEntity> Saved { get { return this._saved; }}
        public Dictionary<Name, DummyEntity> Staged { get{ return this._staged;}}
        public W0List<Dictionary<W0Set<object>, DummyEntity>> SavedRules
        { get { return this._savedRules; } }
        public W0List<Dictionary<W0Set<object>, DummyEntity>> StagedRules
        { get { return this._stagedRules; } }
        public W0List<Dictionary<W0Set<object>, int>> RecycledRules
        { get { return this._recycledRules; } }
    }
}