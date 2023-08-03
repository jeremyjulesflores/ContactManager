namespace ContactManager.API.Helper
{
    public class Difference
    {
        public string PropertyName { get; set;}
        public object OriginalValue { get; set; }
        public object ChangedValue { get; set; }
        
    }
    public static class GetObjectDifference
    {
        public static List<Difference> GetObjectDifferences<T1, T2>(T1 original, T2 changed)
        {
            List<Difference> differences = new List<Difference>();
            var properties1 = typeof(T1).GetProperties();
            var properties2 = typeof(T2).GetProperties();
            foreach (var property1 in properties1)
            {
                var property2 = properties2.FirstOrDefault(p=> p.Name == property1.Name);

                if (property2 != null)
                {
                    var originalValue = property1.GetValue(original);
                    var changedValue = property2.GetValue(changed);

                    if (!Equals(originalValue, changedValue))
                    {
                        differences.Add(new Difference
                        {
                            PropertyName = property1.Name,
                            OriginalValue = originalValue,
                            ChangedValue = changedValue
                        });
                    }
                }
            }
            return differences;
        }
    }
}
