namespace CRMD.Contracts.Orders;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EnOrderType
{
    Table = 1,
    TakeAway = 2
}