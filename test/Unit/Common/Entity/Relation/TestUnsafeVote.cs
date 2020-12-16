using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity.Relation
{
    [TestFixture]
    public class TesUnsafeVote
    {
        private Id _id0;
        private Id _id1;
        private PointTotal _pt;
        private UnsafeVote _vote;

        [OneTimeSetUp]
        public void FirstSetup()
        {
            // Shift them up one to make sure I can savely test both ends
            // without enraging PointTotal.
            UnsafeVote.MaxPoints = new PointTotal(UnsafeVote.MaxPoints.Get+1);
            UnsafeVote.MinPoints = new PointTotal(UnsafeVote.MinPoints.Get+1);
        }

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._pt = new PointTotal(UnsafeVote.MinPoints.Get+1);
            this._vote = new UnsafeVote(this._id0, this._id1, new Id(100), this._pt);
        }

        [Test]
        public void TestStaticMembers()
        {
            Assert.Throws<ArgumentNullException>(()=>UnsafeVote.MinPoints = null);
            Assert.Throws<ArgumentNullException>(()=>UnsafeVote.MaxPoints = null);

            Assert.Throws<ArgumentException>(
                ()=>UnsafeVote.MinPoints = new PointTotal(UnsafeVote.MaxPoints.Get+1));
            Assert.Throws<ArgumentException>(
                ()=>UnsafeVote.MaxPoints = new PointTotal(UnsafeVote.MinPoints.Get-1));
        }

        [Test]
        public void TestPointAssignment()
        {
            Assert.Throws<ArgumentNullException>(()=>this._vote.Points = null);
            Assert.Throws<ArgumentException>(
                ()=>this._vote.Points = new PointTotal(UnsafeVote.MinPoints.Get-1));
            Assert.Throws<ArgumentException>(
                ()=>this._vote.Points = new PointTotal(UnsafeVote.MaxPoints.Get+1));
        }
    }
}