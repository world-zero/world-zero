using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM.Generic.Primary
{
    // NOTE: For readability, tests that test basic, non-rule related
    // functionality are their own test methods, while rule-related tests are
    // separated.
    // NOTE: These tests do not test function member `GenerateId`, and that is
    // done in TestIRAMIdEntityRepo.cs .
    // NOTE: Since DummyEntity can only have set Ids, these tests do not make
    // sure that Ids are not artifacts in entities that are "saved" before a
    // failed save. These tests are performed in TestIRAMIdEntityRepo. For
    // similar reasoning, the tests around FinalChecks() are performed there as
    // well.

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
        private TestRAMEntityRepoAlt _altRepo;

        private Name _idCast(object o)
        {
            return this._repo.TIdCast(o);
        }

        private DummyEntity _entityCast(object o)
        {
            return this._repo.TEntityCast(o);
        }

        private void
        _assertEntitiesEqual(DummyEntity expected, DummyEntity actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Unique, actual.Unique);
            Assert.AreEqual(expected.Combo0, actual.Combo0);
            Assert.AreEqual(expected.Combo1, actual.Combo1);
        }

        private void
        _assertEntitiesEqual(DummyEntity expected, DummyEntityAlt actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Unique, actual.Unique);
            Assert.AreEqual(expected.Combo0, actual.Combo0);
            Assert.AreEqual(expected.Combo1, actual.Combo1);
        }

        private void
        _assertEntitiesEqual(DummyEntity expected, object actual)
        {
            DummyEntity act = actual as DummyEntity;
            DummyEntityAlt actAlt = actual as DummyEntityAlt;
            if (act != null)
                this._assertEntitiesEqual(expected, act);
            else
                this._assertEntitiesEqual(expected, actAlt);
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
            List<Dictionary<W0Set<object>, object>> storedRules,
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
                    this._assertEntitiesEqual(e,
                        this._entityCast(storedRules[i][rule]));
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
            this._altRepo = new TestRAMEntityRepoAlt();

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

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(typeof(DummyEntity).FullName, this._repo.EntityName);
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
        public void TestTxnHappy()
        {
            int y = 0;
            void F(int x)
            {
                y += x;
            }

            Assert.AreEqual(0, y);
            this._repo.Transaction<int>(F, 2);
            Assert.AreEqual(2, y);
        }

        [Test]
        public void TestTxnNullDelegate()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.Transaction<int>(null, 3));
        }

        [Test]
        public void TestTxnFThrowsArgException()
        {
            void ExcF(int x)
            {
                var name0 = new Name("1");
                var name1 = new Name("2");
                var name2 = new Name("3");
                this._repo.Insert(new DummyEntity(name0, 1, 2, 3));
                this._repo.Insert(new DummyEntity(name1, 0, 3, 9));
                this._repo.Save();
                this._repo.Insert(new DummyEntity(name0, 1, 2, 3));
                throw new ArgumentException("sdfasdf");
            }

            this._repo.Clean();
            Assert.Throws<ArgumentException>(()=>
                this._repo.Transaction<int>(ExcF, 3));
            Assert.AreEqual(0, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
        }

        [Test]
        public void TestTxnFThrowsInvalidOpException()
        {
            void ExcF(int x)
            {
                var name0 = new Name("1");
                var name1 = new Name("2");
                var name2 = new Name("3");
                this._repo.Insert(new DummyEntity(name0, 1, 2, 3));
                this._repo.Insert(new DummyEntity(name1, 0, 3, 9));
                this._repo.Save();
                this._repo.Insert(new DummyEntity(name0, 1, 2, 3));
                throw new InvalidOperationException("sdfasdf");
            }

            this._repo.Clean();
            Assert.Throws<InvalidOperationException>(()=>
                this._repo.Transaction<int>(ExcF, 3));
            Assert.AreEqual(0, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
        }

        // NOTE: If this test fails to throw an exception but
        // TestIRAMEntityRepo.TestInsertSaveSameEntity is failing, then this
        // failure is just a symptome of that failure.
        [Test]
        public void TestTxnEndTransactionFails()
        {
            // This will insert an entity with an already saved ID, which will
            // cause Save() to fail in EndTransaction(), all to test that Txn()
            // will catch that exception and throw it's own.

            this._repo.CleanAll();
            this._repo.Insert(new DummyEntity(new Name("a"), 2, 3, 0));
            this._repo.Save();

            void F(int x)
            {
                this._repo.Insert(new DummyEntity(new Name("a"), 2, 3, 0));
            }

            Assert.Throws<ArgumentException>(()=>
                this._repo.Transaction<int>(F, 3));
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
            Assert.AreEqual(
                this._name,
                this._idCast(
                    this._entityCast(this._repo.Staged[this._name]
                ).Id)
            );

            Assert.Throws<ArgumentException>(()=>this._repo.Insert(this._e));
            Assert.Throws<ArgumentException>(()=>this._repo.Insert(
                new DummyEntity(this._name, 34, 43224, 432344)
            ));

            this._repo.Save();
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._assertUniformRuleCounts(1, 0);
            Assert.AreEqual(
                this._name,
                this._idCast(
                    this._entityCast(this._repo.Saved[this._name]
                ).Id)
            );
        }

        [Test]
        public void TestInsertSaveSameEntity()
        {
            // NOTE: I have this issue logged.
            this._repo.Insert(this._e);
            this._repo.Save();
            this._repo.Insert(this._e);
            Assert.Throws<ArgumentException>(
                ()=>this._repo.Save());
        }

        [Test]
        public void TestQuicklyReusingId()
        {
            this._repo.Insert(this._e);
            this._repo.Save();
            this._repo.Delete(this._e.Id);
            this._repo.Insert(this._entities[0]);
            this._repo.Save();
        }

        [Test]
        public void TestSlowlyReusingId()
        {
            this._repo.Insert(this._e);
            this._repo.Save();
            this._repo.Delete(this._e.Id);
            this._repo.Save();
            this._repo.Insert(this._entities[0]);
            this._repo.Save();
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
                this._e,
                this._entityCast(this._repo.StagedRules[1][newRules[1]])
            );

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
                this._e,
                this._entityCast(this._repo.StagedRules[0][newRules[0]])
            );

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
                newEntity,
                this._entityCast(this._repo.Staged[newEntity.Id])
            );
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
                    newDummy,
                    this._entityCast(this._repo.SavedRules[i][ rules[i] ])
                );
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

        [Test]
        public void TestBeginTransaction()
        {
            Assert.IsNull(this._repo.TempData);
            Assert.IsNull(this._altRepo.TempData);
            Assert.AreEqual(0, this._repo.TxnDepth);

            this._repo.BeginTransaction();
            Assert.AreEqual(1, this._repo.TxnDepth);
            Assert.IsNotNull(this._repo.TempData);
            Assert.IsNotNull(this._altRepo.TempData);
            Assert.AreNotEqual(this._repo.TempData, this._repo.Data);
            Assert.AreNotEqual(this._altRepo.TempData, this._altRepo.Data);

            this._repo.BeginTransaction();
            Assert.AreEqual(2, this._repo.TxnDepth);
            Assert.IsNotNull(this._repo.TempData);
            Assert.IsNotNull(this._altRepo.TempData);
            Assert.AreNotEqual(this._repo.TempData, this._repo.Data);
            Assert.AreNotEqual(this._altRepo.TempData, this._altRepo.Data);

            this._repo.DiscardTransaction();
        }

        [Test]
        public void TestDiscardTransaction()
        {
            // Make sure no exc is thrown when no active transaction exists.
            this._repo.DiscardTransaction();

            this._repo.Insert(this._entities[0]);
            this._repo.Save();
            this._repo.Insert(this._entities[1]);
            this._repo.BeginTransaction();
            this._repo.Insert(this._entities[2]);
            this._altRepo.Insert(this._entities[3].ToAlt());
            this._altRepo.DiscardTransaction();
            Assert.AreEqual(0, this._repo.TxnDepth);
            Assert.IsNull(this._repo.TempData);
            Assert.IsNull(this._altRepo.TempData);
            Assert.AreEqual(0, this._altRepo.Staged.Count);
            Assert.AreEqual(1, this._repo.Staged.Count);
            Assert.AreEqual(1, this._repo.Saved.Count);
            this._assertEntitiesEqual(
                this._entities[0],
                this._repo.Saved[this._entities[0].Id]
            );
            this._assertEntitiesEqual(
                this._entities[1],
                this._repo.Staged[this._entities[1].Id]
            );

            this._repo.BeginTransaction();
            this._repo.BeginTransaction();
            this._repo.DiscardTransaction();
            Assert.AreEqual(0, this._repo.TxnDepth);

            this._repo.BeginTransaction();
            this._repo.Insert(this._e);
            this._repo.Save();
            this._altRepo.EndTransaction();
        }

        [Test]
        public void TestEndTransactionHappy()
        {
            this._repo.Insert(this._entities[0]);
            this._repo.Save();
            this._repo.BeginTransaction();

            this._repo.Delete(this._entities[0].Id);
            this._altRepo.Insert(this._entities[0].ToAlt());
            this._repo.EndTransaction();
            Assert.AreEqual(0, this._repo.TxnDepth);
            Assert.IsNull(this._repo.TempData);
            Assert.IsNull(this._altRepo.TempData);
            Assert.AreEqual(0, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            Assert.AreEqual(1, this._altRepo.Saved.Count);
            Assert.AreEqual(0, this._altRepo.Staged.Count);
            this._assertEntitiesEqual(
                this._entities[0],
                this._altRepo.Saved[this._entities[0].Id]
            );

            this._repo.BeginTransaction();
            this._repo.BeginTransaction();
            this._repo.EndTransaction();
            Assert.AreEqual(1, this._repo.TxnDepth);
            Assert.IsNotNull(this._repo.TempData);
            this._repo.EndTransaction();
            Assert.AreEqual(0, this._repo.TxnDepth);

            this._repo.BeginTransaction();
            this._repo.Insert(this._e);
            this._repo.Save();
            this._altRepo.EndTransaction();
        }

        [Test]
        public void TestEndTransactionSad()
        {
            var badEntity = new DummyEntity(
                new Name("bad dummy"),
                this._unique,
                this._combo0,
                this._combo1
            );

            this._repo.Insert(this._e);
            this._repo.Save();
            this._altRepo.Insert(this._e.ToAlt());
            this._altRepo.Save();
            this._repo.BeginTransaction();

            this._repo.Insert(badEntity);
            this._altRepo.Insert(this._entities[0].ToAlt());
            Assert.Throws<ArgumentException>(
                ()=>this._altRepo.EndTransaction());
            Assert.IsNull(this._repo.TempData);
            Assert.IsNull(this._altRepo.TempData);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._assertEntitiesEqual(
                this._e,
                this._repo.Saved[this._e.Id]
            );
            Assert.AreEqual(1, this._altRepo.Saved.Count);
            Assert.AreEqual(0, this._altRepo.Staged.Count);
            this._assertEntitiesEqual(
                this._e,
                this._altRepo.Saved[this._e.Id]
            );

            this._repo.BeginTransaction();
            this._repo.Insert(this._e);
            this._repo.Save();
            this._altRepo.EndTransaction();
        }

        [Test]
        public void TestEndTransactionSadReverse()
        {
            var badEntity = new DummyEntity(
                new Name("bad dummy"),
                this._unique,
                this._combo0,
                this._combo1
            );

            this._repo.Insert(this._e);
            this._repo.Save();
            this._altRepo.Insert(this._e.ToAlt());
            this._altRepo.Save();
            this._repo.BeginTransaction();

            this._altRepo.Insert(badEntity.ToAlt());
            this._repo.Insert(this._entities[0]);
            Assert.Throws<ArgumentException>(
                ()=>this._altRepo.EndTransaction());
            Assert.IsNull(this._repo.TempData);
            Assert.IsNull(this._altRepo.TempData);
            Assert.AreEqual(1, this._repo.Saved.Count);
            Assert.AreEqual(0, this._repo.Staged.Count);
            this._assertEntitiesEqual(
                this._e,
                this._repo.Saved[this._e.Id]
            );
            Assert.AreEqual(1, this._altRepo.Saved.Count);
            Assert.AreEqual(0, this._altRepo.Staged.Count);
            this._assertEntitiesEqual(
                this._e,
                this._altRepo.Saved[this._e.Id]
            );

            this._repo.BeginTransaction();
            this._repo.Insert(this._e);
            this._repo.Save();
            this._altRepo.EndTransaction();
        }

        [Test]
        public void TestIsTransactionActive()
        {
            Assert.IsFalse(this._repo.IsTransactionActive());

            this._repo.BeginTransaction();
            Assert.IsTrue(this._repo.IsTransactionActive());

            this._repo.DiscardTransaction();
            Assert.IsFalse(this._repo.IsTransactionActive());

            this._repo.BeginTransaction();
            Assert.IsTrue(this._repo.IsTransactionActive());

            this._repo.EndTransaction();
            Assert.IsFalse(this._repo.IsTransactionActive());
        }

        [Test]
        public void TestRedeletingASavedEntity()
        {
            this._repo.Insert(this._e);
            this._repo.Save();
            this._repo.Delete(this._e.Id);
            this._repo.Delete(this._e.Id);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.Saved.Count());
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
        : ABCNamedEntity
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

        public override W0List<W0Set<object>> GetUniqueRules()
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

        public DummyEntityAlt ToAlt()
        {
            return new DummyEntityAlt(
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

        public int SavedCount { get { return this._saved.Count; } }
        public int StagedCount { get { return this._staged.Count; } }

        protected override int GetRuleCount()
        {
            var a = new DummyEntity(new Name("foo"), 2, 3, 9);
            return a.GetUniqueRules().Count;
        }

        public Dictionary<object, object> Saved { get { return this._saved; }}
        public Dictionary<object, object> Staged { get{ return this._staged;}}
        public W0List<Dictionary<W0Set<object>, object>> SavedRules
        { get { return this._savedRules; } }
        public W0List<Dictionary<W0Set<object>, object>> StagedRules
        { get { return this._stagedRules; } }
        public W0List<Dictionary<W0Set<object>, int>> RecycledRules
        { get { return this._recycledRules; } }
        public string EntityName { get { return this._entityName; } }
        public Dictionary<string, EntityData> Data { get { return _data; } }
        public Dictionary<string, EntityData> TempData
        { get { return _tempData; } }
        public EntityData InstanceData { get { return _data[this._entityName];}}
        public int TxnDepth { get { return _txnDepth; } }
    }

    /// <summary>
    /// This is a dummy entity to use with testRepoAlt
    /// </summary>
    public class DummyEntityAlt
        : ABCNamedEntity
    {
        public int Unique { get; set; }
        public int Combo0 { get; set; }
        public int Combo1 { get; set; }
        public int Other { get; set; }

        public DummyEntityAlt(
            Name id,
            int unique,
            int combo0,
            int combo1,
            int other=909
        )
            : base(id)
        {
            this.Unique = unique;
            this.Combo0 = combo0;
            this.Combo1 = combo1;
            this.Other = other;
        }

        public override W0List<W0Set<object>> GetUniqueRules()
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
            return new DummyEntityAlt(
                this.Id,
                this.Unique,
                this.Combo0,
                this.Combo1,
                this.Other
            );
        }
    }

    public class TestRAMEntityRepoAlt
        : IRAMEntityRepo<DummyEntityAlt, Name, string>
    {
        public TestRAMEntityRepoAlt()
            : base()
        { }

        protected override int GetRuleCount()
        {
            var a = new DummyEntityAlt(new Name("foo"), 2, 3, 9);
            return a.GetUniqueRules().Count;
        }

        public Dictionary<object, object> Saved { get { return this._saved; }}
        public Dictionary<object, object> Staged { get{ return this._staged;}}
        public W0List<Dictionary<W0Set<object>, object>> SavedRules
        { get { return this._savedRules; } }
        public W0List<Dictionary<W0Set<object>, object>> StagedRules
        { get { return this._stagedRules; } }
        public W0List<Dictionary<W0Set<object>, int>> RecycledRules
        { get { return this._recycledRules; } }
        public string EntityName { get { return this._entityName; } }
        public Dictionary<string, EntityData> Data { get { return _data; } }
        public Dictionary<string, EntityData> TempData
        { get { return _tempData; } }
    }
}