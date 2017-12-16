using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Automata.Z3;
using Microsoft.Z3;


namespace RMC.Tests
{
    public class ArithBoolAlg : Z3BoolAlg
    {
        private InterpolationContext context;

        public ArithBoolAlg(InterpolationContext z3context)
            : base(z3context, z3context.IntSort)
        {
            this.context = z3context;
        }

        public ArithExpr X { get { return (ArithExpr)this.x; } }

        public ArithExpr Y { get { return (ArithExpr)this.y; } }

        public ArithExpr FreshIntVar()
        {
            return (ArithExpr)(context.MkFreshConst("z", context.IntSort));
        }

        public BoolExpr LessThanVal(int i)
        {
            return context.MkLt(this.X, context.MkInt(i));
        }

        public BoolExpr IsEven(int i)
        {
            var z = this.FreshIntVar();
            var xEquals2Z = context.MkEq(X, z + z);
            return context.MkExists(new Expr[] { z }, xEquals2Z);
        }

        public BoolExpr IsOdd()
        {
            var z = this.FreshIntVar();
            var xEquals2ZPlus1 = context.MkEq(X, z + z + 1);
            return context.MkExists(new Expr[] { z }, xEquals2ZPlus1);
        }

        public BoolExpr EqualsVal(int i)
        {
            return this.EqualsVal(context.MkInt(i));
        }

        public BoolExpr EqualsVal(ArithExpr e)
        {
            return context.MkEq(this.X, e);
        }
    }

    [TestClass]
    public class Test
    {
        [TestMethod]
        //Testing Z3 Interpolation
        public void InterpolationTest()
        {
            var context = new InterpolationContext();
            var ba = new ArithBoolAlg(context);

            var eq2 = ba.EqualsVal(2);
            var isOdd = ba.IsOdd();

            Assert.IsTrue(false, ba.ComputeInterpolant(eq2, isOdd).ToString());

            Assert.IsTrue(false);
        }

        [TestMethod]
        //Testing Z3 Symbolic Application
        public void SymbolicallyApplyTest()
        {
            var context = new InterpolationContext();
            var ba = new ArithBoolAlg(context);

            var eq2 = ba.EqualsVal(2);
            var isOdd = ba.IsOdd();
            var xEqYPlus2 = ba.EqualsVal(ba.Y + 2);

            Console.WriteLine(isOdd.ToString());
            Console.WriteLine(xEqYPlus2.ToString());

            Assert.IsTrue(false, ba.SymbolicallyApply(xEqYPlus2,isOdd).ToString());
        }
    }
}
