namespace CRMD.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class IgnoreOnAttribute : Attribute
    {
        public enOperationMode operationMode { get; }

        public IgnoreOnAttribute(enOperationMode OperationMode)
        {
            operationMode = OperationMode;
        }
    }
}