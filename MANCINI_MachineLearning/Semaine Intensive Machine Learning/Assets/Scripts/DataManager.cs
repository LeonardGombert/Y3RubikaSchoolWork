using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;
using System.Text;
using Leonard;

namespace Leonard
{
    [XmlRoot("Data")]
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance;
        public string path;

        //the xmlSerializer uses the Data class
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Data));
        //encoding type
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        StreamWriter streamWriter;

        private void Awake()
        {
            if (instance != null)
                Destroy(instance);

            else instance = this;

            SetPath();
        }

        [ContextMenu("Yeet")]
        public void SetPath()
        {
            //path = a combination of the location of this application + this file
            path = Path.Combine(Application.persistentDataPath, "Data.xml");
        }

        public void Save(List<NeuralNetwork> _neuralNetworks)
        {
            streamWriter = new StreamWriter(path, false, encoding);

            /* vu que la classe est vide, on doit linker les deux listes
             * les crochets servent de "constructeur intégré" à la déclaration
            */
            Data data = new Data { neuralNetworks = _neuralNetworks };

            xmlSerializer.Serialize(streamWriter, data);
        }

        //fonction de type Data car la fonction doit nous retourner une liste de Neural Networks
        public Data Load()
        {
            if (File.Exists(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                return xmlSerializer.Deserialize(fileStream) as Data;
            }

            else return null;
        }
    }
}