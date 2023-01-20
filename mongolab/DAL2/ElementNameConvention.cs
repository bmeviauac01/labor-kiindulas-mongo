using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Bme.Swlab1.Mongo.Dal;

public class ElementNameConvention : CamelCaseElementNameConvention, IMemberMapConvention
{
    void IMemberMapConvention.Apply(BsonMemberMap memberMap)
    {
        string name = memberMap.MemberName;
        var firstLowercaseIndex = FindFirstLowercaseIndex(name);

        switch (firstLowercaseIndex)
        {
            case < 0:
                memberMap.SetElementName(name.ToLowerInvariant());
                break;
            case > 1:
                memberMap.SetElementName(name.Substring(0, firstLowercaseIndex - 1).ToLowerInvariant() + name.Substring(firstLowercaseIndex - 1));
                break;
            default:
                Apply(memberMap);
                break;
        }
    }

    private static int FindFirstLowercaseIndex(string name)
    {
        for (int i = 0; i < name.Length; i++)
        {
            if (char.IsLower(name, i))
            {
                return i;
            }
        }

        return -1;
    }
}