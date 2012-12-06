// NUnit + Moq: test the rollup behavior with a mocked store. No database.
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

[TestFixture]
public class RollupTests
{
    [Test]
    public void Sums_bytes_for_a_customer()
    {
        var store = new Mock<IReportStore>();
        store.Setup(s => s.DailyBytes(4821)).Returns(new List<long> { 100, 200, 50 });

        var service = new RollupService(store.Object);
        long total = service.TotalBytes(4821);

        Assert.AreEqual(350, total);
        store.Verify(s => s.DailyBytes(4821), Times.Once);
    }
}

public interface IReportStore { IEnumerable<long> DailyBytes(int customerId); }

public class RollupService
{
    private readonly IReportStore _store;
    public RollupService(IReportStore store) { _store = store; }
    public long TotalBytes(int id)
    {
        long t = 0;
        foreach (var b in _store.DailyBytes(id)) t += b;
        return t;
    }
}
