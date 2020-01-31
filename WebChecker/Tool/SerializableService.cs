using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WebChecker.Tool
{
    public class SerializableService<T>
    {
        private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebChecker\\conf.cfg");
        public void Serialize(T obj)
        {
            try
            {
                if (obj != null)
                {

                    using (var fs = new FileStream(path, FileMode.Create))
                    {

                        var bf = new BinaryFormatter();

                        bf.Serialize(fs, obj);

                    }

                }
            }
            catch (Exception e)
            {
                Error.ShowError(e.Message);
            }

        }

        public T Deserialize()
        {

            Debug.WriteLine("Wejscie");
            try
            {
                T temp = default(T);

                if (File.Exists(path))
                {

                    using (var fs = new FileStream(path, FileMode.Open))
                    {

                        if (fs.Length > 0)
                        {

                            var bf = new BinaryFormatter();

                            return (T)bf.Deserialize(fs);

                        }

                    }

                }

                return temp;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                //Error.ShowError(e.Message);
                return default(T);
            }
            finally
            {
                Debug.WriteLine("Wyjscie");
            }
        }
    }
}
