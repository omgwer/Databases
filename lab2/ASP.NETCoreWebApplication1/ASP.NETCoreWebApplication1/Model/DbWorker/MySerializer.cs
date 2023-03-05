using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ASP.NETCoreWebApplication1.Model.DbWorker
{
    public static class MySerializer
    {
        public static Stream SerializeToStream(object o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            formatter.Serialize(stream, o);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            return stream;
        }

        public static object DeserializeFromStream(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            object o = formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            return o;
        }
    }
}
