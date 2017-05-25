using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace silentFailure
{
    public class Class1
    {
        [Fact]
        public void ThisShouldFail()
        {
            Assert.True(false);
        }

        [Fact]
        public void ThisShouldPass()
        {
            Assert.True(true);
        }

    }
}
