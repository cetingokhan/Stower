using Microsoft.Extensions.Options;
using Moq;
using Stower.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Stower.Tests
{
    public class UnitTest1
    {

        public class Foo
        {
            public int FooInt { get; set; }
        }

        [Fact]
        public void ToppleCount()
        {
            var optionsMoq = new Mock<IOptions<StowerOptions>>();
            optionsMoq.Setup(x => x.Value).Returns(new StowerOptions()
            {
                MaxStackLenght = 100,
                MaxWaitInSecond = 6000,
                Stacks = new System.Collections.Generic.List<Base.ICustomStack>()
                 {
                     new CustomStack<Foo>()
                 }
            });

            var toppleHandlerMoq = new Mock<IToppleHandler>();

            BaseStower baseStower = new BaseStower(optionsMoq.Object, toppleHandlerMoq.Object);

            Parallel.For(0, 1000, async x =>
            {
                await baseStower.Add<Foo>(new Foo()
                {
                    FooInt = x
                });
            });

            toppleHandlerMoq.Verify(x => x.Handle(It.Is<List<object>>(x=> x.Count == 100)), Times.Exactly(10));
        }


        [Fact]
        public void MissingTopple()
        {
            var optionsMoq = new Mock<IOptions<StowerOptions>>();
            optionsMoq.Setup(x => x.Value).Returns(new StowerOptions()
            {
                MaxStackLenght = 100,
                MaxWaitInSecond = 6000,
                Stacks = new System.Collections.Generic.List<Base.ICustomStack>()
                 {
                     new CustomStack<Foo>()
                 }
            });




            var toppleHandlerMoq = new Mock<IToppleHandler>();

            BaseStower baseStower = new BaseStower(optionsMoq.Object, toppleHandlerMoq.Object);

            Parallel.For(0, 999, async x =>
            {
                await baseStower.Add<Foo>(new Foo()
                {
                    FooInt = x
                });
            });

            toppleHandlerMoq.Verify(x => x.Handle(It.IsAny<List<object>>()), Times.Exactly(9));
        }

    }
}
