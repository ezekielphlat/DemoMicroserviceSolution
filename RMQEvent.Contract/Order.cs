namespace RMQEvent.Contract
{
   public record RMQOrder(string OrderId, string CustomerId, string OrderedOn, List<RMQOrderDetial> OrderDetail);
    public record RMQOrderDetial(string ProductId, string Quantity, string UnitPrice);
}