using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Relation
{
    [TestFixture]
    public class TestVote
    {
        private Id _id0;
        private Id _id1;
        private PointTotal _pt;
        private Vote _vote;

        [OneTimeSetUp]
        public void FirstSetup()
        {
            // Shift them up one to make sure I can savely test both ends
            // without enraging PointTotal.
            Vote.MaxPoints = new PointTotal(Vote.MaxPoints.Get+1);
            Vote.MinPoints = new PointTotal(Vote.MinPoints.Get+1);
        }

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._pt = new PointTotal(Vote.MinPoints.Get+1);
            this._vote = new Vote(this._id0, this._id1, new Id(100), this._pt);
        }

        [Test]
        public void TestStaticMembers()
        {
            Assert.Throws<ArgumentNullException>(()=>Vote.MinPoints = null);
            Assert.Throws<ArgumentNullException>(()=>Vote.MaxPoints = null);

            Assert.Throws<ArgumentException>(
                ()=>Vote.MinPoints = new PointTotal(Vote.MaxPoints.Get+1));
            Assert.Throws<ArgumentException>(
                ()=>Vote.MaxPoints = new PointTotal(Vote.MinPoints.Get-1));
        }

        [Test]
        public void TestPointAssignment()
        {
            Assert.Throws<ArgumentNullException>(()=>this._vote.Points = null);
            Assert.Throws<ArgumentException>(
                ()=>this._vote.Points = new PointTotal(Vote.MinPoints.Get-1));
            Assert.Throws<ArgumentException>(
                ()=>this._vote.Points = new PointTotal(Vote.MaxPoints.Get+1));
        }
    }
}